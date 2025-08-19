import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUserRequest } from '../dto/requests/user/create-user-request';
import { Observable } from 'rxjs';
import { CreateUserResponse } from '../dto/responses/user/create-user-response';
import { environment } from '../environments/environment';
import { LoginRequest } from '../dto/requests/user/login-request';
import { LoginResponse } from '../dto/responses/user/login-response';
import { SaveUserChangesRequest } from '../dto/requests/user/save-user-changes-request';
import { SaveUserChangesResponse } from '../dto/responses/user/save-user-changes-response';
import { GetUserResponse } from '../dto/responses/user/get-user-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  createUser(request: CreateUserRequest): Observable<CreateUserResponse> {
    return this.http.post<CreateUserResponse>(
      `${this.apiUrl}/auth/register`,
      request
    );
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/auth/login`,
      request
    );
  }

  updateUser(request: SaveUserChangesRequest): Observable<SaveUserChangesResponse> {
    return this.http.put<SaveUserChangesResponse>(
      `${environment.apiUrl}/user/save-changes`,
      request
    );
  }

  getUser(): Observable<GetUserResponse> {
    return this.http.get<GetUserResponse>(`${environment.apiUrl}/user/get-user`);
  }
}