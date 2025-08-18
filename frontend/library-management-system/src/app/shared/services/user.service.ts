import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUserRequest } from '../dto/requests/user/create-user-request';
import { Observable } from 'rxjs';
import { CreateUserResponse } from '../dto/responses/user/create-user-response';
import { environment } from '../environments/environment';
import { LoginRequest } from '../dto/requests/user/login-request';
import { LoginResponse } from '../dto/responses/user/login-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiUrl = `${environment.apiUrl}/auth/register`; // prilagodi backend ruti

  constructor(private http: HttpClient) {}

  createUser(request: CreateUserRequest): Observable<CreateUserResponse> {
    return this.http.post<CreateUserResponse>(`${this.apiUrl}`, request);
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, request);
  }
}
