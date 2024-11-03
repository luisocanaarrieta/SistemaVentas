import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  constructor() { }


  obtenerCategorias(): Observable<any> {
    return new Observable<any>(observer => {
      observer.next([
        { idRol: 1, nombreRol: 'Administrador' },
        { idRol: 2, nombreRol: 'Usuario' }
      ]);
      observer.complete();
    });
  }
}
