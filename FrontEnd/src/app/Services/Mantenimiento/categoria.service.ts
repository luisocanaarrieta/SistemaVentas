import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {


  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient)   { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Categoria';
  }

  obtenerCategorias():Observable<any>  {
    return this.http.get(this.myAppUrl + this.myApiUrl+'/ListarCategoriaProductos');
  }
}
