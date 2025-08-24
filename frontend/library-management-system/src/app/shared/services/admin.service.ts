import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AddAuthorRequest } from '../dto/requests/admin/add-author-request';
import { Observable } from 'rxjs';
import { AddAuthorResponse } from '../dto/responses/admin/add-author-response';
import { AddGenreRequest } from '../dto/requests/admin/add-genre-request';
import { AddGenreResponse } from '../dto/responses/admin/add-genre-response';
import { AddBookRequest } from '../dto/requests/admin/add-book-request';
import { AddBookResponse } from '../dto/responses/admin/add-book-response';
import { DeleteBookRequest } from '../dto/requests/admin/delete-book-request';
import { BookListResponse } from '../dto/responses/admin/book-list-response';
import { UserListResponse } from '../dto/responses/admin/user-list-response';
import { UserDetailsResponse } from '../dto/responses/admin/user-details-response';
import { EditBookRequest } from '../dto/requests/admin/edit-book-request';
import { SaveUserChangesRequest } from '../dto/requests/user/save-user-changes-request';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly apiUrl = `${environment.apiUrl}/admin`;

  constructor(private http: HttpClient) {}

  addAuthor(request: AddAuthorRequest): Observable<AddAuthorResponse> {
    return this.http.post<AddAuthorResponse>(`${this.apiUrl}/author`, request);
  }

  addGenre(request: AddGenreRequest): Observable<AddGenreResponse> {
    return this.http.post<AddGenreResponse>(`${this.apiUrl}/genre`, request);
  }

  addBook(request: AddBookRequest): Observable<AddBookResponse> {
    return this.http.post<AddBookResponse>(`${this.apiUrl}/books`, request);
  }

  deleteBook(request: DeleteBookRequest): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/books`, { body: request });
  }

  getAllBooks(): Observable<BookListResponse[]> {
    return this.http.get<BookListResponse[]>(`${this.apiUrl}/books`);
  }

  getAllUsers(): Observable<UserListResponse[]> {
    return this.http.get<UserListResponse[]>(`${this.apiUrl}/users`);
  }

  getUserDetails(userId: string): Observable<UserDetailsResponse> {
    return this.http.get<UserDetailsResponse>(`${this.apiUrl}/users/${userId}`);
  }

  getAllAuthors(): Observable<AddAuthorResponse[]> {
    return this.http.get<AddAuthorResponse[]>(`${this.apiUrl}/authors`);
  }

  getAllGenres(): Observable<AddGenreResponse[]> {
    return this.http.get<AddGenreResponse[]>(`${this.apiUrl}/genres`);
  }

  updateBook(request: EditBookRequest): Observable<AddBookResponse> {
    return this.http.put<AddBookResponse>(`${this.apiUrl}/books`, request);
  }

  getBookDetails(bookId: string): Observable<AddBookResponse> {
    return this.http.get<AddBookResponse>(`${this.apiUrl}/books/${bookId}`);
  }

  deleteAuthor(authorId: string): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/author`, { body: { authorId } });
  }

  deleteGenre(genreId: string): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/genre`, { body: { genreId } });
  }

  saveUserChanges(userId: string, request: SaveUserChangesRequest): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiUrl}/users/save-changes/${userId}`, request);
  }
}
