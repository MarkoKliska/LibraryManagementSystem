import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddAuthorResponse } from '../../../../shared/dto/responses/admin/add-author-response';
import { AddGenreResponse } from '../../../../shared/dto/responses/admin/add-genre-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { AddBookRequest } from '../../../../shared/dto/requests/admin/add-book-request';

@Component({
  selector: 'app-add-book',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.scss'
})
export class AddBookComponent implements OnInit {
  addBookForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  authors: AddAuthorResponse[] = [];
  genres: AddGenreResponse[] = [];

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private loaderService: LoaderService,
    private toastService: ToastService
  ) {
    this.addBookForm = this.fb.group({
      title: ['', Validators.required],
      authorId: ['', Validators.required],
      genreId: ['', Validators.required],
      isbn13: ['', [Validators.required, Validators.pattern(/^\d{13}$/)]],
      numberOfCopies: [1, [Validators.required, Validators.min(1)]]
    });
  }

  get f() { return this.addBookForm.controls; }

  ngOnInit(): void {
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

    this.adminService.getAllGenres().subscribe({
      next: (genres) => {
        this.genres = genres;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load genres.';
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to load genres.', 'Error');
      }
    });
  }

  onSubmit(): void {
    if (this.addBookForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.successMessage = null;

    const request: AddBookRequest = this.addBookForm.value;

    this.loaderService.startLoading();
    this.adminService.addBook(request).subscribe({
      next: () => {
        this.successMessage = 'Book added successfully!';
        this.addBookForm.reset();
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.toastService.showSuccess(this.successMessage, 'Success');
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to add book.';
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to add book.', 'Error');
      }
    });
  }
}
