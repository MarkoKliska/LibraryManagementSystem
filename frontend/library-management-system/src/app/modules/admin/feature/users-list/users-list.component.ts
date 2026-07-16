import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { UserListResponse } from '../../../../shared/dto/responses/admin/user-list-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users-list',
  imports: [],
  templateUrl: './users-list.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent implements OnInit {
  users: UserListResponse[] = [];
  errorMessage: string | null = null;

  page = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 0;
  readonly pageSizeOptions = [10, 20, 50];

  constructor(
    private adminService: AdminService,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loaderService.startLoading();
    this.adminService.getAllUsers(this.page, this.pageSize).subscribe({
      next: (result) => {
        this.totalCount = result.totalCount;
        this.totalPages = result.totalPages;

        if (result.items.length === 0 && this.page > 1) {
          this.page = this.page - 1;
          this.loadUsers();
          return;
        }

        this.users = result.items;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load users.';
        this.loaderService.stopLoading();
        if (this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to load users.', 'Error');
      }
    });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages || page === this.page) return;
    this.page = page;
    this.loadUsers();
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.page = 1;
    this.loadUsers();
  }

  viewUserDetails(userId: string): void {
    this.router.navigate(['admin', 'users', userId]);
  }
}