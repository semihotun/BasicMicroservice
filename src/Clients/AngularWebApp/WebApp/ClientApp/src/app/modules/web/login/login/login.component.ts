import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForLoginDto } from 'src/app/core/models/identityService/userForLoginDto';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { TokenLocalStorage } from 'src/app/core/services/token-local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginDto: UserForLoginDto = new UserForLoginDto();
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private alertifyService: AlertifyService,
    private tokenLocalStorage:TokenLocalStorage
  ) {}

  ngOnInit(): void {
    this.createLoginForm();
    if(this.authService.Islogged()){
      this.router.navigateByUrl("admin");
    }
  }

  createLoginForm(){
    this.loginForm = this.formBuilder.group({
      email: ["", Validators.required],
      password: ["", Validators.required],
    })
  }

  
  login() {
    if (this.loginForm.valid) {
      this.loginDto = Object.assign({}, this.loginForm.value);

      this.authService.login(this.loginDto).subscribe(data => {
        this.tokenLocalStorage.setToken(data.token);
        this.alertifyService.success("Giriş Yapıldı");
        this.router.navigateByUrl("admin");
      }, ((error: any) => {
        console.log(error);
        this.alertifyService.error(error["error"]);
      }));

      this.clearFormGroup(this.loginForm);
    }
  }


  clearFormGroup(group: FormGroup) {
    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach(key => {
      group.get(key)?.setErrors(null);
      if (key == 'id')
        group.get(key)?.setValue(0);
    });
  }


}


