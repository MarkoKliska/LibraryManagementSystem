import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../shared/ui/navbar/navbar.component';
import { RentedBookResponse } from '../../shared/dto/responses/book/rented-book-response';
import { BookListResponse } from '../../shared/dto/responses/book/book-list-response';
import { BookService } from '../../shared/services/book.service';
import { LoaderService } from '../../shared/services/loader.service';
import { ToastService } from '../../shared/services/toast.service';
import { CommonModule } from '@angular/common';
import { RentBookRequest } from '../../shared/dto/requests/book/rent-book-request';
import { ReturnBookRequest } from '../../shared/dto/requests/book/return-book-request';
import { SearchBooksRequest } from '../../shared/dto/requests/book/search-book-request';
import { SearchBooksResponse } from '../../shared/dto/responses/book/search-book-response';
import { ViewToggleComponent } from './ui/view-toggle/view-toggle.component';
import { BookCardComponent } from './ui/book-card/book-card.component';
import { SearchComponent } from './ui/search/search.component';

@Component({
  selector: 'app-dashboard',
  imports: [
    NavbarComponent,
    CommonModule,
    ViewToggleComponent,
    BookCardComponent,
    SearchComponent
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  viewMode: 'available' | 'rented' = 'available';
  availableBooks: SearchBooksResponse[] = [];
  rentedBooks: RentedBookResponse[] = [];
  errorMessage: string | null = null;

  constructor(
    private bookService: BookService,
    private loaderService: LoaderService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.loadAvailableBooks();
  }

  switchView(mode: 'available' | 'rented'): void {
    this.viewMode = mode;
    this.errorMessage = null;
    if (mode === 'available') {
      this.loadAvailableBooks();
    } else {
      this.loadRentedBooks();
    }
  }

  loadAvailableBooks(): void {
    const emptyRequest: SearchBooksRequest = {};
    this.onSearch(emptyRequest);
  }

  onSearch(request: SearchBooksRequest): void {
    this.loaderService.startLoading();
    this.bookService.searchBooks(request).subscribe({
      next: (books) => {
        this.availableBooks = books.filter(book => book.availableCopies > 0);
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load available books.';
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to load available books.', 'Error');
      }
    });
  }

  loadRentedBooks(): void {
    this.loaderService.startLoading();
    this.bookService.getRentedBooks().subscribe({
      next: (books) => {
        this.rentedBooks = books;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load rented books.';
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to load rented books.', 'Error');
      }
    });
  }

  rentBook(bookId: string): void {
    this.loaderService.startLoading();
    const request: RentBookRequest = { bookId };
    this.bookService.rentBook(request).subscribe({
      next: (response) => {
        this.loaderService.stopLoading();
        this.toastService.showSuccess(response.message || 'You have rented a book successfully.', 'Success');
        this.loadAvailableBooks();
        if (this.viewMode === 'rented') {
          this.loadRentedBooks();
        }
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to rent book.';
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to rent book.', 'Error');
      }
    });
  }

  returnBook(rentalId: string): void {
    this.loaderService.startLoading();
    const request: ReturnBookRequest = { rentalId };
    this.bookService.returnBook(request).subscribe({
      next: (response) => {
        this.loaderService.stopLoading();
        this.toastService.showSuccess(response.message || 'Book successfully returned.', 'Success');
        this.loadRentedBooks();
        if (this.viewMode === 'available') {
          this.loadAvailableBooks();
        }
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to return book.';
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to return book.', 'Error');
      }
    });
  }
}