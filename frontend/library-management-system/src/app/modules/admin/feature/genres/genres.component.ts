
import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { AddGenreRequest } from '../../../../shared/dto/requests/admin/add-genre-request';
import { AddGenreResponse } from '../../../../shared/dto/responses/admin/add-genre-response';
import { DeleteGenreRequest } from '../../../../shared/dto/requests/admin/delete-genre-request';

@Component({
  selector: 'app-add-genre',
  imports: [
    ReactiveFormsModule
],
  templateUrl: './genres.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './genres.component.scss'
})
export class GenresComponent implements OnInit, OnDestroy {
  addGenreForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  genres: AddGenreResponse[] = [];

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
    this.addGenreForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.searchSubject
      .pipe(debounceTime(300), distinctUntilChanged())
      .subscribe((term) => {
        this.searchTerm = term;
        this.page = 1;
        this.loadGenres();
      });
    this.loadGenres();
  }

  ngOnDestroy(): void {
    this.searchSubject.complete();
  }

  get f() { return this.addGenreForm.controls; }

  loadGenres(): void {
    this.loaderService.startLoading();
    this.adminService.getGenresPaged(this.page, this.pageSize, this.searchTerm).subscribe({
      next: (result) => {
        this.totalCount = result.totalCount;
        this.totalPages = result.totalPages;

        if (result.items.length === 0 && this.page > 1) {
          this.page = this.page - 1;
          this.loadGenres();
          return;
        }

        this.genres = result.items;
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

  onSearchChange(value: string): void {
    this.searchSubject.next(value);
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages || page === this.page) return;
    this.page = page;
    this.loadGenres();
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.page = 1;
    this.loadGenres();
  }

  onSubmit(): void {
    if (this.addGenreForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.successMessage = null;

    const request: AddGenreRequest = this.addGenreForm.value;

    this.loaderService.startLoading();
    this.adminService.addGenre(request).subscribe({
      next: (response) => {
        this.successMessage = 'Genre added successfully!';
        this.addGenreForm.reset();
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.toastService.showSuccess(this.successMessage, 'Success');
        this.loadGenres();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to add genre.';
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        if (this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to add genre.', 'Error');
      }
    });
  }

  deleteGenre(genreId: string): void {
    if (!confirm('Are you sure you want to delete this genre?')) return;
    const request: DeleteGenreRequest = {
       genreId
      };
    this.loaderService.startLoading();
    this.adminService.deleteGenre(request).subscribe({
      next: () => {
        this.toastService.showSuccess('Genre deleted successfully!', 'Success');
        this.loadGenres();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to delete genre.';
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to delete genre.', 'Error');
      }
    });
  }
}