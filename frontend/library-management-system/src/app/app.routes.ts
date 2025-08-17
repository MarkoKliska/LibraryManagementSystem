import { Routes } from '@angular/router';
import { RouteNames } from './shared/consts/routes';
import { LandingPageComponent } from './modules/landing-page/landing-page.component';
import { NoAuthGuard } from './shared/guards/no-auth.guard';
import { AuthGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
  { path: RouteNames.Landing, component: LandingPageComponent, canActivate: [NoAuthGuard] },
  { path: RouteNames.Login, loadComponent: () => import('./modules/user/features/login/login.component').then(m => m.LoginComponent), canActivate: [NoAuthGuard] },
  { path: RouteNames.Register, loadComponent: () => import('./modules/user/features/register/register.component').then(m => m.RegisterComponent), canActivate: [NoAuthGuard] },
  { path: RouteNames.Dashboard, loadComponent: () => import('./modules/dashboard/dashboard.component').then(m => m.DashboardComponent), canActivate: [AuthGuard] },
  { path: '**', redirectTo: RouteNames.Landing }
];
