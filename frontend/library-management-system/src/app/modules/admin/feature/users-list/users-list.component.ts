import { Component, OnInit } from '@angular/core';
import { UserListResponse } from '../../../../shared/dto/responses/admin/user-list-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users-list',
  imports: [
    CommonModule
  ],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent implements OnInit {
  users: UserListResponse[] = [];
  errorMessage: string | null = null;

  constructor(
    private adminService: AdminService,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loaderService.startLoading();
    this.adminService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load users.';
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else 
           this.toastService.showError('Failed to load users.', 'Error');
      }
    });
  }

  viewUserDetails(userId: string): void {
    this.router.navigate(['admin', 'users', userId]);
  }
}
