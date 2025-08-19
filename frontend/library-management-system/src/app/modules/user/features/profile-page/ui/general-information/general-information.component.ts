import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../../../shared/services/user.service';
import { SaveUserChangesRequest } from '../../../../../../shared/dto/requests/user/save-user-changes-request';
import { CommonModule } from '@angular/common';
import { GetUserResponse } from '../../../../../../shared/dto/responses/user/get-user-response';
import { LoaderService } from '../../../../../../shared/services/loader.service';
import { ToastService } from '../../../../../../shared/services/toast.service';

@Component({
  selector: 'app-general-information',
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './general-information.component.html',
  styleUrl: './general-information.component.scss'
})
export class GeneralInformationComponent implements OnInit {
  generalInfoForm: FormGroup;
  isSubmitting = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private loaderService: LoaderService,
    private toastService: ToastService
  ) {
    this.generalInfoForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get f() { return this.generalInfoForm.controls; }

  ngOnInit(): void {
    this.loaderService.startLoading();
    this.userService.getUser().subscribe({
      next: (user: GetUserResponse) => {
        this.generalInfoForm.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email
        });
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load user data.';
        this.loaderService.stopLoading();
      }
    });
  }

  onSubmit(): void {
    if (this.generalInfoForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.successMessage = null;

    const request: SaveUserChangesRequest = this.generalInfoForm.value;

    this.loaderService.startLoading();

    this.userService.updateUser(request).subscribe({
      next: (response) => {
        this.successMessage = 'Your profile has been updated successfully!';
        this.generalInfoForm.patchValue(response);
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.toastService.showSuccess(this.successMessage, 'Success');
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Update failed. Please try again.';
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.toastService.showError(err.error?.error, 'Error');
      }
    });
  }
}
