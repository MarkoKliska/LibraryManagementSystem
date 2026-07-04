import { Component, EventEmitter, Output } from '@angular/core';
import { SearchBooksRequest } from '../../../../shared/dto/requests/book/search-book-request';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  imports: [
    FormsModule
  ],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent {
  @Output() searchEvent = new EventEmitter<SearchBooksRequest>();

  title: string = '';
  author: string = '';
  genre: string = '';
  isbn: string = '';

  onSearch() {
    const request: SearchBooksRequest = {
      title: this.title || undefined,
      authorName: this.author || undefined,
      genreName: this.genre || undefined,
      isbn13: this.isbn || undefined
    };
    this.searchEvent.emit(request);
  }
}