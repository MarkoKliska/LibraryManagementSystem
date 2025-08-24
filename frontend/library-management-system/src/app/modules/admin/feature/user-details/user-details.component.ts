import { Component, OnInit } from '@angular/core';
import { UserDetailsResponse } from '../../../../shared/dto/responses/admin/user-details-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { ActivatedRoute } from '@angular/router';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { CommonModule } from '@angular/common';
import { SaveUserChangesRequest } from '../../../../shared/dto/requests/admin/save-user-changes-request';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss'
})
export class UserDetailsComponent implements OnInit {
  user: UserDetailsResponse | null = null;
  editForm: FormGroup;
  isEditing = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  isSubmitting = false;
  userId: string | null = null;

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private fb: FormBuilder
  ) {
    this.editForm = this.fb.group({
      firstName: [''],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('userId');
    if (this.userId) {
      this.loadUserDetails(this.userId);
    }
  }

  get f() { return this.editForm.controls; }

  loadUserDetails(userId: string): void {
    this.loaderService.startLoading();
    this.adminService.getUserDetails(userId).subscribe({
      next: (user) => {
        this.user = user;
        this.editForm.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email
        });
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load user details.';
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to load user details.', 'Error');
      }
    });
  }

  toggleEdit(): void {
    this.isEditing = !this.isEditing;
    this.errorMessage = null;
    this.successMessage = null;
    if (!this.isEditing && this.user) {
      this.editForm.patchValue({
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email
      });
    }
  }

  onSubmit(): void {
    if (this.editForm.invalid || !this.userId) return;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.successMessage = null;

    const request: SaveUserChangesRequest = {
      firstName: this.editForm.value.firstName,
      lastName: this.editForm.value.lastName,
      email: this.editForm.value.email
    };

    this.loaderService.startLoading();
    this.adminService.saveUserChanges(this.userId, request).subscribe({
      next: () => {
        this.successMessage = 'User details updated successfully!';
        this.isSubmitting = false;
        this.isEditing = false;
        this.loaderService.stopLoading();
        this.toastService.showSuccess(this.successMessage, 'Success');
        this.loadUserDetails(this.userId!);
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to update user details.';
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to update user details.', 'Error');
      }
    });
  }
}
