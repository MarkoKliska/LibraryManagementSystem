import { Component, OnInit } from '@angular/core';
import { AddAuthorRequest } from '../../../../shared/dto/requests/admin/add-author-request';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { CommonModule } from '@angular/common';
import { AddAuthorResponse } from '../../../../shared/dto/responses/admin/add-author-response';

@Component({
  selector: 'app-add-author',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './authors.component.html',
  styleUrl: './authors.component.scss'
})
export class AuthorsComponent implements OnInit {
  addAuthorForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  authors: AddAuthorResponse[] = [];

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private loaderService: LoaderService,
    private toastService: ToastService
  ) {
    this.addAuthorForm = this.fb.group({
      firstName: [''],
      lastName: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAuthors();
  }

  get f() { return this.addAuthorForm.controls; }

  loadAuthors(): void {
    this.loaderService.startLoading();
    this.adminService.getAllAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load authors.';
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to load authors.', 'Error');
      }
    });
  }

  onSubmit(): void {
    if (this.addAuthorForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.successMessage = null;

    const request: AddAuthorRequest = this.addAuthorForm.value;

    this.loaderService.startLoading();
    this.adminService.addAuthor(request).subscribe({
      next: (response) => {
        this.successMessage = 'Author added successfully!';
        this.addAuthorForm.reset();
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.toastService.showSuccess(this.successMessage, 'Success');
        this.loadAuthors(); 
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to add author.';
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        if (this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to add author.', 'Error');
      }
    });
  }

  deleteAuthor(authorId: string): void {
    if (!confirm('Are you sure you want to delete this author?')) return;

    this.loaderService.startLoading();
    this.adminService.deleteAuthor(authorId).subscribe({
      next: () => {
        this.authors = this.authors.filter(a => a.id !== authorId);
        this.loaderService.stopLoading();
        this.toastService.showSuccess('Author deleted successfully!', 'Success');
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to delete author.';
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to delete author.', 'Error');
      }
    });
  }
}
