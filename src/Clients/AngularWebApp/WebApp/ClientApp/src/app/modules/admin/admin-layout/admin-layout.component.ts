import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  constructor(
    private authService:AuthService,
    private router:Router,
    private alertifyService:AlertifyService
    ) { }

  ngOnInit(): void {
  }

  logout(){
    this.authService.logout();
    this.alertifyService.success("Çıkış Yapıldı");
    this.router.navigateByUrl("login");
  }

}
