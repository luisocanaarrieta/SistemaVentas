import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/Seguridad/login.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(
    private _loginService: LoginService,
  ) { }
  
    ngOnInit(): void {
      this.obtenerNombreUsuario();
    }
  
    obtenerNombreUsuario(): void {
      console.log(this._loginService.getTokenDecode(), 'token decode');
    }
}
