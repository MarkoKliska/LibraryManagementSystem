import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../../../shared/services/user.service';
import { LoaderService } from '../../../../../../shared/services/loader.service';
import { ToastService } from '../../../../../../shared/services/toast.service';
import { AuthService } from '../../../../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { DeleteAccountRequest } from '../../../../../../shared/dto/requests/user/delete-account-request';
import { RouteNames } from '../../../../../../shared/consts/routes';

@Component({
  selector: 'app-delete-account',
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './delete-account.component.html',
  styleUrl: './delete-account.component.scss'
})
export class DeleteAccountComponent {
  deleteAccountForm: FormGroup;
  isSubmitting = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private authService: AuthService,
    private router: Router
  ) {
    this.deleteAccountForm = this.fb.group({
      password: ['', [Validators.required]]
    });
  }

  get f() { return this.deleteAccountForm.controls; }

  onSubmit(): void {
    if (this.deleteAccountForm.invalid) return;

    //DELETE THIS AFTER YOU ADD COMMOM DIALOG
    if (!confirm('Are you sure you want to delete your account? This action cannot be undone.')) {
      return;
    }

    this.isSubmitting = true;
    this.loaderService.startLoading();

    const request: DeleteAccountRequest = {
      password: this.f['password'].value
    };

    this.userService.deleteAccount(request).subscribe({
      next: () => {
        this.toastService.showSuccess('Account deleted successfully!', 'Success');
        this.authService.removeToken();
        this.isSubmitting = false;
        this.loaderService.stopLoading();
        this.router.navigate([RouteNames.Login]);
      },
      error: (err) => {
        const msg = err.error?.error || 'Failed to delete account.';
        this.toastService.showError(msg, 'Error');
        this.isSubmitting = false;
        this.loaderService.stopLoading();
      }
    });
  }
}
