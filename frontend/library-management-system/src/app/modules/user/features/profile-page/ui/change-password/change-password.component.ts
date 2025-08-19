import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../../../shared/services/user.service';
import { LoaderService } from '../../../../../../shared/services/loader.service';
import { ToastService } from '../../../../../../shared/services/toast.service';
import { ChangePasswordRequest } from '../../../../../../shared/dto/requests/user/change-password-request';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-change-password',
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {
  changePasswordForm: FormGroup;
  isSubmitting = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private loaderService: LoaderService,
    private toastService: ToastService
  ) {
    this.changePasswordForm = this.fb.group({
      oldPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordsMatch });
  }

  get f() { return this.changePasswordForm.controls; }

  private passwordsMatch(form: FormGroup) {
    const newPass = form.get('newPassword')?.value;
    const confirmPass = form.get('confirmPassword')?.value;
    return newPass === confirmPass ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.changePasswordForm.invalid) return;

    this.isSubmitting = true;
    this.loaderService.startLoading();

    const request: ChangePasswordRequest = {
      oldPassword: this.f['oldPassword'].value,
      newPassword: this.f['newPassword'].value
    };

    this.userService.changePassword(request).subscribe({
      next: () => {
        this.toastService.showSuccess('Password changed successfully!', 'Success');
        this.changePasswordForm.reset();
        this.isSubmitting = false;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        const msg = err.error?.error || 'Failed to change password.';
        this.toastService.showError(msg, 'Error');
        this.isSubmitting = false;
        this.loaderService.stopLoading();
      }
    });
  }
}
