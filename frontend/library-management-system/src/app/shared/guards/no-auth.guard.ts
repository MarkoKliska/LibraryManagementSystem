import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class NoAuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('token');
    if (!token) {
      return true;
    }

    try {
      const decoded: any = jwtDecode(token);
      const now = Math.floor(Date.now() / 1000);

      if (decoded.exp && decoded.exp > now) {
        // token još važi → preusmeri na dashboard
        this.router.navigate(['/dashboard']);
        return false;
      } else {
        // token istekao → ukloni ga
        localStorage.removeItem('token');
        return true;
      }
    } catch (e) {
      // token nije validan
      localStorage.removeItem('token');
      return true;
    }
  }
}
