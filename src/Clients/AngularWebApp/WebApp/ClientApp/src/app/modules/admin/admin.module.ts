import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from 'src/app/core/interceptors/auth-interceptor';


export const AdminRoutes: Routes = [
  { path: 'home', component: AdminHomeComponent ,canActivate:[AuthGuard]},
  { path: 'admin', component: AdminLayoutComponent, canActivate: [AuthGuard] }
]

@NgModule({
  declarations: [
    AdminHomeComponent,
    AdminLayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(AdminRoutes),
  ],
  providers:[
    {
      provide:HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi:true
    }
  ]
})
export class AdminModule { }
