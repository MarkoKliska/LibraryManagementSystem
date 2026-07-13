import { Component, EventEmitter, Input, Output, ChangeDetectionStrategy } from '@angular/core';
import { RentedBookResponse } from '../../../../shared/dto/responses/book/rented-book-response';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-card',
  imports: [
    CommonModule
  ],
  templateUrl: './book-card.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './book-card.component.scss'
})
export class BookCardComponent {
  @Input() book!: any;
  @Input() type: 'available' | 'rented' = 'available';
  @Output() action = new EventEmitter<string>();
}