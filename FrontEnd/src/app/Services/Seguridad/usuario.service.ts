import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient)   { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Usuario';
  }

  saveUser(usuario : any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl +'/SaveUser', usuario);
  }

  changePassword(changePassword: any):Observable<any>  {
    return this.http.put(this.myAppUrl + this.myApiUrl + '/CambiarPassword', changePassword);
  }

  ListarUsuarios():Observable<any>  {
    return this.http.get(this.myAppUrl + this.myApiUrl  + '/ListarUsuarios');
  }

  EliminarUsuario(userId: number):Observable<any>  {
    return this.http.put(this.myAppUrl + this.myApiUrl + '/EliminarUsuario', userId);
  }

  ActualizarUsuario(usuario : any):Observable<any>  {
    return this.http.post(this.myAppUrl + this.myApiUrl + '/ActualizarUsuario', usuario);
  }

}
