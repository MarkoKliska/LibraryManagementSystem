import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RouteNames } from '../../shared/consts/routes';
@Component({
  selector: 'app-landing-page',
  imports: [],
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent {
  constructor(
    private router: Router
  ) {}

  onNavigateToLogin(){
    this.router.navigate([RouteNames.Login]);
  }

  onNavigateToRegister(){
    this.router.navigate([RouteNames.Register]);
  }
}
