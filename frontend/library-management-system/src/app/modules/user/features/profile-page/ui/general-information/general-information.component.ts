import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../../../shared/services/user.service';
import { SaveUserChangesRequest } from '../../../../../../shared/dto/requests/user/save-user-changes-request';
import { CommonModule } from '@angular/common';
import { GetUserResponse } from '../../../../../../shared/dto/responses/user/get-user-response';

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

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.generalInfoForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get f() { return this.generalInfoForm.controls; }

  ngOnInit(): void {
    this.userService.getUser().subscribe({
      next: (user: GetUserResponse) => {
        this.generalInfoForm.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email
        });
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load user data.';
      }
    });
  }

  onSubmit(): void {
    if (this.generalInfoForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.successMessage = null;

    const request: SaveUserChangesRequest = this.generalInfoForm.value;

    this.userService.updateUser(request).subscribe({
      next: (response) => {
        this.successMessage = 'Your profile has been updated successfully!';
        this.generalInfoForm.patchValue(response); // response treba da ima iste property-je
        this.isSubmitting = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Update failed. Please try again.';
        this.isSubmitting = false;
      }
    });
  }
}
