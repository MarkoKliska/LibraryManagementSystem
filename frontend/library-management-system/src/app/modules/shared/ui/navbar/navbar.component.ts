import { Component } from '@angular/core';
import { AuthService } from '../../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { RouteNames } from '../../../../shared/consts/routes';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  goToDashboard(): void {
    this.router.navigate([RouteNames.Dashboard]);
  }

  goToProfile(): void {
    this.router.navigate([RouteNames.Profile]);
  }

  logout(): void {
    this.authService.removeToken();
    this.router.navigate([RouteNames.Login]);
  }
}
