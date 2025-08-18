import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './profile-page.component';
import { GeneralInformationComponent } from './ui/general-information/general-information.component';
import { ChangePasswordComponent } from './ui/change-password/change-password.component';
import { DeleteAccountComponent } from './ui/delete-account/delete-account.component';
import { ProfilePageRoutingModule } from './profile-page-routing.module';
@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    ProfilePageRoutingModule,
    ProfilePageComponent,
    GeneralInformationComponent,
    ChangePasswordComponent,
    DeleteAccountComponent
  ]
})
export class ProfilePageModule { }
