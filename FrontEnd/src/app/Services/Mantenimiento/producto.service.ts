import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient)   { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Producto';
  }

  ListarProductos():Observable<any>  {
    return this.http.get(this.myAppUrl + this.myApiUrl+'/ListarProductos');
  }
  
  InsertarProducto(usuario : any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl +'/InsertarProducto', usuario);
  }

  EliminarProducto(userId: number):Observable<any>  {
    return this.http.put(this.myAppUrl + this.myApiUrl + '/EliminarProducto', userId);
  }

  ActualizarProducto(usuario : any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl + '/ActualizarProducto', usuario);
  }

}