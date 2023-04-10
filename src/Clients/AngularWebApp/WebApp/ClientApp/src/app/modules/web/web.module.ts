import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { HomeLayoutComponent } from './homeLayout/home-layout/home-layout.component';
import { LoginComponent } from './login/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AdminHomeComponent } from '../admin/admin-home/admin-home.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';

export const WebRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'homelayout', component: HomeLayoutComponent },
  { path: 'login',component:LoginComponent}
];

@NgModule({
  declarations: [
    HomeComponent,
    HomeLayoutComponent,
    LoginComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(WebRoutes)
  ],
  providers: [
    AuthService,
    AlertifyService
  ]
})
export class WebModule { }
