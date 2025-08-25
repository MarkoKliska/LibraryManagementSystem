import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RentedBookResponse } from '../../../../shared/dto/responses/book/rented-book-response';
import { BookListResponse } from '../../../../shared/dto/responses/book/book-list-response';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-card',
  imports: [
    CommonModule
  ],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.scss'
})
export class BookCardComponent {
  @Input() book!: BookListResponse | RentedBookResponse;
  @Input() viewMode!: 'available' | 'rented';
  @Output() rent = new EventEmitter<string>();
  @Output() return = new EventEmitter<string>();

  isRentedBook(book: BookListResponse | RentedBookResponse): book is RentedBookResponse {
    return this.viewMode === 'rented';
  }

  onAction(): void {
    if (this.viewMode === 'available') {
      this.rent.emit((this.book as BookListResponse).id);
    } else {
      this.return.emit((this.book as RentedBookResponse).rentalId);
    }
  }
}
