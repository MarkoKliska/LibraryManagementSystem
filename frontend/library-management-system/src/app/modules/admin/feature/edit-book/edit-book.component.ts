import { Component } from '@angular/core';
import { EditBookRequest } from '../../../../shared/dto/requests/admin/edit-book-request';
import { AddAuthorResponse } from '../../../../shared/dto/responses/admin/add-author-response';
import { AddGenreResponse } from '../../../../shared/dto/responses/admin/add-genre-response';
import { AdminService } from '../../../../shared/services/admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { AddBookResponse } from '../../../../shared/dto/responses/admin/add-book-response';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-book',
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './edit-book.component.html',
  styleUrl: './edit-book.component.scss'
})
export class EditBookComponent {
  book: EditBookRequest = {
    bookId: '',
    title: '',
    authorId: '',
    genreId: '',
    isbn13: '',
    numberOfCopies: 1
  };
  authors: AddAuthorResponse[] = [];
  genres: AddGenreResponse[] = [];
  errorMessage: string | null = null;

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute,
    public router: Router,
    private loaderService: LoaderService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    const bookId = this.route.snapshot.paramMap.get('bookId');
    if (bookId) {
      this.book.bookId = bookId;
      this.loadBookDetails(bookId);
      this.loadAuthorsAndGenres();
    } else {
      this.errorMessage = 'Invalid book ID.';
      this.toastService.showError(this.errorMessage, 'Error');
    }
  }

  private loadBookDetails(bookId: string): void {
    this.loaderService.startLoading();
    this.adminService.getBookDetails(bookId).subscribe({
      next: (book: AddBookResponse) => {
        this.book = {
          bookId: book.id,
          title: book.title,
          authorId: book.authorId,
          genreId: book.genreId,
          isbn13: book.isbn13,
          numberOfCopies: book.numberOfCopies
        };
        this.loaderService.stopLoading();
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load book details.';
        this.loaderService.stopLoading();
        if (this.errorMessage) {
          this.toastService.showError(this.errorMessage, 'Error');
        }
      }
    });
  }

  private loadAuthorsAndGenres(): void {
    this.adminService.getAllAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load authors.';
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to load authors.', 'Error');
      }
    });

    this.adminService.getAllGenres().subscribe({
      next: (genres) => {
        this.genres = genres;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to load genres.';
        if(this.errorMessage)
          this.toastService.showError(this.errorMessage, 'Error');
        else
          this.toastService.showError('Failed to load genres.', 'Error');
      }
    });
  }

  updateBook(): void {
    if (!this.validateForm()) {
      return;
    }

    this.loaderService.startLoading();
    this.adminService.updateBook(this.book).subscribe({
      next: () => {
        this.loaderService.stopLoading();
        this.toastService.showSuccess('Book updated successfully!', 'Success');
        this.router.navigate(['admin', 'books']);
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Failed to update book.';
        this.loaderService.stopLoading();
        if (this.errorMessage) {
          this.toastService.showError(this.errorMessage, 'Error');
        }
      }
    });
  }

  private validateForm(): boolean {
    if (!this.book.title.trim()) {
      this.errorMessage = 'Title is required.';
      this.toastService.showError(this.errorMessage, 'Error');
      return false;
    }
    if (!this.book.authorId) {
      this.errorMessage = 'Author is required.';
      this.toastService.showError(this.errorMessage, 'Error');
      return false;
    }
    if (!this.book.genreId) {
      this.errorMessage = 'Genre is required.';
      this.toastService.showError(this.errorMessage, 'Error');
      return false;
    }
    if (!this.book.isbn13 || this.book.isbn13.length !== 13) {
      this.errorMessage = 'ISBN13 must be 13 characters long.';
      this.toastService.showError(this.errorMessage, 'Error');
      return false;
    }
    if (this.book.numberOfCopies < 1) {
      this.errorMessage = 'Number of copies must be at least 1.';
      this.toastService.showError(this.errorMessage, 'Error');
      return false;
    }
    return true;
  }
}
