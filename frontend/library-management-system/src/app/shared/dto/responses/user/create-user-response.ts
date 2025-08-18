export interface CreateUserResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  role: string;
  token?: string; // za sada opciono, dodaćemo kad backend bude slao
}

