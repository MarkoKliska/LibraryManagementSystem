import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BookListResponse } from '../../../../shared/dto/responses/admin/book-list-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { Router } from '@angular/router';
import { DeleteBookRequest } from '../../../../shared/dto/requests/admin/delete-book-request';
import { DeleteBookResponse } from '../../../../shared/dto/responses/admin/delete-book-response';

@Component({
  selector: 'app-books-list',
  imports: [
    CommonModule
  ],
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.scss'
})
export class BooksListComponent implements OnInit {
  books: BookListResponse[] = [];
  errorMessage: string | null = null;

  constructor(
    private adminService: AdminService,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loaderService.startLoading();
    this.adminService.getAllBooks().subscribe({
      next: (books) => {
        this.books = books;
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

  editBook(bookId: string): void {
    this.router.navigate(['admin', 'books', 'edit', bookId]);
  }

  deleteBook(bookId: string): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.loaderService.startLoading();
      const request: DeleteBookRequest = { bookId };
      this.adminService.deleteBook(request).subscribe({
        next: (response: DeleteBookResponse) => {
          this.books = this.books.filter(book => book.id !== bookId);
          this.loaderService.stopLoading();
          this.toastService.showSuccess(response.message || 'Book deleted successfully.', 'Success');
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
