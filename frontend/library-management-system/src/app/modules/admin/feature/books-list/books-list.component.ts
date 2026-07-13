import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { BookListResponse } from '../../../../shared/dto/responses/admin/book-list-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { Router } from '@angular/router';
import { DeleteBookRequest } from '../../../../shared/dto/requests/admin/delete-book-request';
import { DeleteBookResponse } from '../../../../shared/dto/responses/admin/delete-book-response';

@Component({
  selector: 'app-books-list',
  imports: [],
  templateUrl: './books-list.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './books-list.component.scss'
})
export class BooksListComponent implements OnInit {
  books: BookListResponse[] = [];
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
    this.loadBooks();
  }

  loadBooks(): void {
    this.loaderService.startLoading();
    this.adminService.getAllBooks(this.page, this.pageSize).subscribe({
      next: (result) => {
        this.totalCount = result.totalCount;
        this.totalPages = result.totalPages;

        if (result.items.length === 0 && this.page > 1) {
          this.page = this.page - 1;
          this.loadBooks();
          return;
        }

        this.books = result.items;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load books.';
        this.loaderService.stopLoading();
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to load books.','Error')
      }
    });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages || page === this.page) return;
    this.page = page;
    this.loadBooks();
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.page = 1;
    this.loadBooks();
  }

  editBook(bookId: string): void {
    this.router.navigate(['admin', 'books', 'edit', bookId]);
  }

  deleteBook(bookId: string): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.loaderService.startLoading();
      const request: DeleteBookRequest = { bookId };
      this.adminService.deleteBook(request).subscribe({
        next: (response: DeleteBookResponse) => {
          this.toastService.showSuccess(response.message || 'Book deleted successfully.', 'Success');
          this.loadBooks();
        },
        error: (err) => {
          this.errorMessage = err.error?.error || 'Failed to delete book.';
          this.loaderService.stopLoading();
          if(this.errorMessage)
            this.toastService.showError(this.errorMessage, 'Error');
          else
            this.toastService.showError('Failed to delete book.', 'Error');
        }
      });
    }
  }
}