import { Component } from '@angular/core';
import { NavbarComponent } from '../../../shared/ui/navbar/navbar.component';
import { ProfilePageRoutingModule } from './profile-page-routing.module';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-profile-page',
  imports: [
    NavbarComponent,
    RouterOutlet
],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.scss'
})
export class ProfilePageComponent {

}
