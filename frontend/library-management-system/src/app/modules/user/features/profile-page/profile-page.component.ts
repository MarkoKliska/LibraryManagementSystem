import { Component } from '@angular/core';
import { NavbarComponent } from '../../../shared/ui/navbar/navbar.component';
import { ProfilePageRoutingModule } from './profile-page-routing.module';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { RouteNames } from '../../../../shared/consts/routes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile-page',
  imports: [
    NavbarComponent,
    RouterOutlet,
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.scss'
})
export class ProfilePageComponent {
  RouteNames = RouteNames;

  constructor(
    private router: Router,
  ) {}

  goGeneralInfo() {
    this.router.navigate([RouteNames.Profile, RouteNames.GeneralInformationRoute]);
  }

  goChangePassword() {
    this.router.navigate([RouteNames.Profile, RouteNames.ChangePasswordRoute]);
  }

  goDeleteAccount() {
    this.router.navigate([RouteNames.Profile, RouteNames.DeleteMyAccountRoute]);
  }
}
