import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { NavbarComponent } from '../shared/ui/navbar/navbar.component';
import { RentedBookResponse } from '../../shared/dto/responses/book/rented-book-response';
import { BookService } from '../../shared/services/book.service';
import { LoaderService } from '../../shared/services/loader.service';
import { ToastService } from '../../shared/services/toast.service';

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
    ViewToggleComponent,
    BookCardComponent,
    SearchComponent
],
  templateUrl: './dashboard.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  viewMode: 'available' | 'rented' = 'available';
  availableBooks: SearchBooksResponse[] = [];
  rentedBooks: RentedBookResponse[] = [];
  errorMessage: string | null = null;

  lastSearch: SearchBooksRequest = {};
  page = 1;
  pageSize = 12;
  totalCount = 0;
  totalPages = 0;
  readonly pageSizeOptions = [12, 24, 48];

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
    this.onSearch({});
  }

  onSearch(request: SearchBooksRequest): void {
    this.lastSearch = request;
    this.page = 1;
    this.fetchBooks();
  }

  fetchBooks(): void {
    this.loaderService.startLoading();
    this.bookService.searchBooks(this.lastSearch, this.page, this.pageSize).subscribe({
      next: (result) => {
        this.totalCount = result.totalCount;
        this.totalPages = result.totalPages;

        if (result.items.length === 0 && this.page > 1) {
          this.page = this.page - 1;
          this.fetchBooks();
          return;
        }

        this.availableBooks = result.items;
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load available books.';
        this.loaderService.stopLoading();
        this.toastService.showError(this.errorMessage || 'Failed to load available books.', 'Error');
      }
    });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages || page === this.page) return;
    this.page = page;
    this.fetchBooks();
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.page = 1;
    this.fetchBooks();
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
        this.toastService.showSuccess(response.message || 'You have rented a book successfully.', 'Success');
        this.fetchBooks();
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
        this.toastService.showSuccess(response.message || 'Book successfully returned.', 'Success');
        this.loadRentedBooks();
        if (this.viewMode === 'available') {
          this.fetchBooks();
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