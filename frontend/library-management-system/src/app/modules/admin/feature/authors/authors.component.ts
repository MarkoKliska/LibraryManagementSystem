import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { AddAuthorRequest } from '../../../../shared/dto/requests/admin/add-author-request';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

import { AddAuthorResponse } from '../../../../shared/dto/responses/admin/add-author-response';
import { DeleteAuthorRequest } from '../../../../shared/dto/requests/admin/delete-author-request';

@Component({
  selector: 'app-add-author',
  imports: [
    ReactiveFormsModule
],
  templateUrl: './authors.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './authors.component.scss'
})
export class AuthorsComponent implements OnInit, OnDestroy {
  addAuthorForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  authors: AddAuthorResponse[] = [];

  page = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 0;
  searchTerm = '';
  readonly pageSizeOptions = [10, 20, 50];

  private searchSubject = new Subject<string>();

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
    this.searchSubject
      .pipe(debounceTime(300), distinctUntilChanged())
      .subscribe((term) => {
        this.searchTerm = term;
        this.page = 1;
        this.loadAuthors();
      });
    this.loadAuthors();
  }

  ngOnDestroy(): void {
    this.searchSubject.complete();
  }

  get f() { return this.addAuthorForm.controls; }

  loadAuthors(): void {
    this.loaderService.startLoading();
    this.adminService.getAuthorsPaged(this.page, this.pageSize, this.searchTerm).subscribe({
      next: (result) => {
        this.totalCount = result.totalCount;
        this.totalPages = result.totalPages;

        if (result.items.length === 0 && this.page > 1) {
          this.page = this.page - 1;
          this.loadAuthors();
          return;
        }

        this.authors = result.items;
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

  onSearchChange(value: string): void {
    this.searchSubject.next(value);
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages || page === this.page) return;
    this.page = page;
    this.loadAuthors();
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.page = 1;
    this.loadAuthors();
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
    const request: DeleteAuthorRequest = {
      authorId: authorId
    }
    this.loaderService.startLoading();
    this.adminService.deleteAuthor(request).subscribe({
      next: () => {
        this.toastService.showSuccess('Author deleted successfully!', 'Success');
        this.loadAuthors();
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