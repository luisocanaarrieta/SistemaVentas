import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private snack:MatSnackBar) { }

  mensajeAlerta(message: string, action: string) {
    this.snack.open(message, action, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000,
    });
  }
}
