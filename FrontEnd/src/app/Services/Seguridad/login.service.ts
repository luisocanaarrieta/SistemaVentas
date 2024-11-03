import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient) 
  { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Login/ValidateUser';
  }

  login(usuario : any):Observable<any>
  {
    return this.http.post(this.myAppUrl + this.myApiUrl, usuario);
  }

  setLocalStorage(data: string) :void
  {
    localStorage.setItem('token',data);
  }

  getTokenDecode(): any {
    const helper = new JwtHelperService();
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = helper.decodeToken(token);
      return decodedToken;
    }
    return null;
  }
  

  removeLocalStorage(): void
  {
    localStorage.removeItem('token');
  }
}
