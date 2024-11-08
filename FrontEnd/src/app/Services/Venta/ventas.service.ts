import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VentasService {
  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient)   { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Venta';
  }

  RegistrarVenta(usuario : any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl + '/RegistrarVenta', usuario);
  }

  ObtenerVentasConDetalle(venta :any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl + '/ObtenerVentasConDetalle', venta);
  }

  ListarEstadosReparto():Observable<any>  {
    return this.http.get(this.myAppUrl + this.myApiUrl  + '/ListarEstadosReparto');
  }

  CambiarEstadoVenta(estado : any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl + '/CambiarEstadoVenta', estado);
  }


}
