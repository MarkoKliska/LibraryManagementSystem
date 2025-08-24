import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './genres.component.html',
  styleUrl: './genres.component.scss'
})
export class GenresComponent implements OnInit {
  addGenreForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  genres: AddGenreResponse[] = [];

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
    this.loadGenres();
  }

  get f() { return this.addGenreForm.controls; }

  loadGenres(): void {
    this.loaderService.startLoading();
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
        this.genres = this.genres.filter(g => g.id !== genreId);
        this.loaderService.stopLoading();
        this.toastService.showSuccess('Genre deleted successfully!', 'Success');
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