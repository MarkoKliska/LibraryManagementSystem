import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BookListResponse} from '../dto/responses/book/book-list-response';
import { Observable } from 'rxjs';
import { RentedBookResponse } from '../dto/responses/book/rented-book-response';
import { RentBookResponse } from '../dto/responses/book/rent-book-response';
import { ReturnBookRequest } from '../dto/requests/book/return-book-request';
import { RentBookRequest } from '../dto/requests/book/rent-book-request';
import { ReturnBookResponse } from '../dto/responses/book/return-book-response';
import { SearchBooksRequest } from '../dto/requests/book/search-book-request';
import { SearchBooksResponse } from '../dto/responses/book/search-book-response';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private readonly apiUrl = `${environment.apiUrl}/book`;

  constructor(private http: HttpClient) {}

  getAllBooks(): Observable<BookListResponse[]> {
    return this.http.get<BookListResponse[]>(`${this.apiUrl}/all`);
  }

  getRentedBooks(): Observable<RentedBookResponse[]> {
    return this.http.get<RentedBookResponse[]>(`${this.apiUrl}/rented`);
  }

  rentBook(request: RentBookRequest): Observable<RentBookResponse> {
    return this.http.post<RentBookResponse>(`${this.apiUrl}/rent`, request);
  }

  returnBook(request: ReturnBookRequest): Observable<ReturnBookResponse> {
    return this.http.post<ReturnBookResponse>(`${this.apiUrl}/return`, request);
  }

  searchBooks(request: SearchBooksRequest): Observable<SearchBooksResponse[]> {
    let params = new HttpParams();
    if (request.title) {
      params = params.set('title', request.title);
    }
    if (request.authorName) {
      params = params.set('authorName', request.authorName);
    }
    if (request.genreName) {
      params = params.set('genreName', request.genreName);
    }
    if (request.isbn13) {
      params = params.set('isbn13', request.isbn13);
    }

    return this.http.get<BookListResponse[]>(`${this.apiUrl}/search`, { params });
  }
}
