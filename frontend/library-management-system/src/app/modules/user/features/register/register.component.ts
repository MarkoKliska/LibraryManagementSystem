import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../shared/services/user.service';
import { AuthService } from '../../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { CreateUserRequest } from '../../../../shared/dto/requests/user/create-user-request';
import { RouteNames } from '../../../../shared/consts/routes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  registerForm: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;

  get firstName() { return this.registerForm.get('firstName'); }
  get lastName() { return this.registerForm.get('lastName'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  onSubmit(): void {
    if (this.registerForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;

    const request: CreateUserRequest = this.registerForm.value;

    this.userService.createUser(request).subscribe({
      next: (response) => {
        if (response.token) {
          this.authService.setToken(response.token);
        }
        this.router.navigate([RouteNames.Dashboard]);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Registration failed. Try again.';
        this.isSubmitting = false;
      },
    });
  }
}
