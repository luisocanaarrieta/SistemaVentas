import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

import { LoginService } from 'src/app/Services/Seguridad/login.service';
import { SharedService } from 'src/app/Shared/shared.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formularioLogin: FormGroup;

  ocultarPassword = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private spinner: NgxSpinnerService,
    private _sharedService: SharedService,
    private _loginService: LoginService) {
      
    this.formularioLogin = this.fb.group({
      usuario: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  ngOnInit(): void {
  }
  
  iniciarSesion() {
    const usuario: any = {
      userUserName: this.formularioLogin.value.usuario,
      userPassword: this.formularioLogin.value.password,
    }
    
    this.spinner.show();

    this._loginService.login(usuario).subscribe(data => {
      console.log(data);
      this._loginService.setLocalStorage(data.token);
      this.router.navigate(['/pages/dashboard']);
      this.spinner.hide();
    }, error => {
      console.log(error);
      this.spinner.hide();
      this._sharedService.mensajeAlerta('Usuario o contraseña incorrecta',"Error");
      this.formularioLogin.reset();
    })
  }
}
