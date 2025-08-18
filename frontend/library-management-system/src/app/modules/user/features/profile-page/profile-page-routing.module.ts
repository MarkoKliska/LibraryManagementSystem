import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProfilePageComponent } from './profile-page.component';
import { RouteNames } from '../../../../shared/consts/routes';
import { GeneralInformationComponent } from './ui/general-information/general-information.component';
import { DeleteAccountComponent } from './ui/delete-account/delete-account.component';
import { ChangePasswordComponent } from './ui/change-password/change-password.component';


const routes: Routes = [
  {
    path: '',
    component: ProfilePageComponent,
    children: [
      { path: RouteNames.GeneralInformationRoute, component: GeneralInformationComponent },
      { path: RouteNames.ChangePasswordRoute, component: ChangePasswordComponent },
      { path: RouteNames.DeleteMyAccountRoute, component: DeleteAccountComponent },
      { path: '', redirectTo: RouteNames.GeneralInformationRoute, pathMatch: 'full' }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilePageRoutingModule { }
