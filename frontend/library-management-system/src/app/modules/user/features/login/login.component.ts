import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../shared/services/user.service';
import { AuthService } from '../../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../../../../shared/dto/requests/user/login-request';
import { LoginResponse } from '../../../../shared/dto/responses/user/login-response';
import { RouteNames } from '../../../../shared/consts/routes';

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;

    const request: LoginRequest = this.loginForm.value;

    this.userService.login(request).subscribe({
      next: (response: LoginResponse) => {
        if (response.token) {
          this.authService.setToken(response.token);
          this.router.navigate([RouteNames.Dashboard]);
        }
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Login failed. Check credentials.';
        this.isSubmitting = false;
      }
    });
  }
}
