import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeLayoutComponent } from './modules/web/homeLayout/home-layout/home-layout.component';
import { AdminLayoutComponent } from './modules/admin/admin-layout/admin-layout.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {
    path:'',
    component:HomeLayoutComponent,
    children:[
      {
        path:'',
        loadChildren: () => import('./modules/web/web.module').then(m => m.WebModule) 
      }
    ]
  },
  {
    path:'admin',
    component:AdminLayoutComponent,
    children:[
      {
        path:'',
        loadChildren:()=>import('./modules/admin/admin.module').then(m=>m.AdminModule)
      }
    ],
    canActivate:[AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
