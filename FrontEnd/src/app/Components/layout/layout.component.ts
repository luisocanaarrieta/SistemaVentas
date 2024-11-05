import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/Services/Seguridad/login.service';
import { SharedService } from 'src/app/Shared/shared.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  listaMenu: any[] = [];
  rolUsuario: string = '';
  subUsuario: string = '';

  constructor(
    private router: Router,
    private _sharedService: SharedService,
    private _loginService: LoginService,

  ) { }


  ngOnInit(): void {
    this.obtenerNombreUsuario();
  }

  obtenerNombreUsuario(): void {
    const tokenDecode = this._loginService.getTokenDecode();
    this.subUsuario = tokenDecode.sub;
    this.rolUsuario = tokenDecode.userRole; //string
  }

  cerrarSesion(): void {
    this.router.navigate(['/login']);
  }


}
