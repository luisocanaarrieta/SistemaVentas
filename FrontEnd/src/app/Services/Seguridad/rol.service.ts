import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RolService {


  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient)   { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Rol';
  }

  obtenerRoles():Observable<any>  {
    return this.http.get(this.myAppUrl + this.myApiUrl+'/obtenerListaRoles');
  }

}
