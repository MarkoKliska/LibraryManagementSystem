import { Component } from '@angular/core';
import { AuthService } from '../../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { RouteNames } from '../../../../shared/consts/routes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [
    CommonModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  goToDashboard(): void {
    this.router.navigate([RouteNames.Dashboard]);
  }

  goToProfile(): void {
    this.router.navigate([RouteNames.Profile]);
  }

  goToAdminPanel(): void {
    this.router.navigate([RouteNames.Admin]);
  }

  logout(): void {
    this.authService.removeToken();
    this.router.navigate([RouteNames.Login]);
  }
}
