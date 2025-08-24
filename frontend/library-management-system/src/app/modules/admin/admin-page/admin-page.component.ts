import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../../shared/ui/navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-page',
  imports: [
    NavbarComponent,
    RouterOutlet,
    CommonModule,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.scss'
})
export class AdminPageComponent {
  constructor(private router: Router) {}

  goToUsers() {
    this.router.navigate(['admin', 'users']);
  }

  goToBooks() {
    this.router.navigate(['admin', 'books']);
  }

  goToAuthors() {
    this.router.navigate(['admin', 'authors']);
  }

  goToGenres() {
    this.router.navigate(['admin', 'genres']);
  }

  goToAddBook() {
    this.router.navigate(['admin', 'add-book']);
  }
}
