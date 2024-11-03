import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/Seguridad/login.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
  constructor(
    private _loginService: LoginService,
  ) { }
  
    ngOnInit(): void {
      this.obtenerNombreUsuario
    }
  
    obtenerNombreUsuario(): void {
      console.log(this._loginService.getTokenDecode(), 'token decode');
    }
}
