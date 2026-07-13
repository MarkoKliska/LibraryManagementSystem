import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class AdminGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['/login']);
      return false;
    }

    try {
      const decoded: any = jwtDecode(token);

      const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      if (role === 'Admin') {
        return true;
      }

      this.router.navigate(['/dashboard']);
      return false;

    } catch (e) {
      localStorage.removeItem('token');
      this.router.navigate(['/login']);
      return false;
    }
  }
}
