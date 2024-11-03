import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RolService } from 'src/app/Services/Seguridad/rol.service';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';


@Component({
  selector: 'app-modal-usuario',
  templateUrl: './modal-usuario.component.html',
  styleUrls: ['./modal-usuario.component.css']
})
export class ModalUsuarioComponent implements OnInit {

  formularioUsuario: FormGroup
  tituloAccion: string = "Agregar";
  botonAccion: string = "Guardar";

  listaRoles: any[] = [];

  constructor(
    private modalActual: MatDialogRef<ModalUsuarioComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private _rolService: RolService,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
  ) {

    this.formularioUsuario = this.fb.group({
      nombreCompleto: ['', Validators.required],
      nombreUsuario: ['', Validators.required],
      password: ['', Validators.required],
      idRol: [0, Validators.required],
      email: ['', Validators.required],
      telefono: ['', Validators.required],
      esActivo: ['1', Validators.required],
    });

    if (this.data != null) {
      this.tituloAccion = "Editar";
      this.botonAccion = "Actualizar";
    }

    this._rolService.obtenerRoles().subscribe(r => {
      this.listaRoles = r;
    })
  }


  ngOnInit(): void {
    if (this.data != null) {
      this.formularioUsuario.patchValue({
        nombreCompleto: this.data.nombreCompleto,
        nombreUsuario: this.data.nombreUsuario,
        password: this.data.password,
        idRol: this.data.idRol,
        email: this.data.email,
        telefono: this.data.telefono,
        esActivo: this.data.esActivo.toString(),
      });
    }
  }


  guardarEditarUsuario() {
    const usuario: any = {
      idUsuario: this.data != null ? this.data.idUsuario : 0,
      nombreCompleto: this.formularioUsuario.value.nombreCompleto,
      nombreUsuario: this.formularioUsuario.value.nombreUsuario,
      password: this.formularioUsuario.value.password,
      idRol: this.formularioUsuario.value.idRol,
      email: this.formularioUsuario.value.email,
      telefono: this.formularioUsuario.value.telefono,
      esActivo: parseInt(this.formularioUsuario.value.esActivo),
    }

    if (this.data == null) {
      this._usuarioService.saveUser(usuario).subscribe({
        next: (r) => {
          if (r.status == 200) {
            this._sharedService.mensajeAlerta("Usuario guardado correctamente", "Exito");
            this.modalActual.close("true");
          }
          else {
            this._sharedService.mensajeAlerta("Error al guardar el usuario", "Error");
          }
        },
        error: (err) => {
          this._sharedService.mensajeAlerta("Error al guardar el usuario", "Error");
        }
      }
      );

    } else {
      this._usuarioService.updateUser(usuario).subscribe({
        next: (r) => {
          if (r.status == 200) {
            this._sharedService.mensajeAlerta("Usuario actualizado correctamente", "Exito");
            this.modalActual.close("true");
          }
          else {
            this._sharedService.mensajeAlerta("Error al actualizar el usuario", "Error");
          }
        },
        error: (err) => {
          this._sharedService.mensajeAlerta("Error al actualizar el usuario", "Error");
        }
      }
      );
    }
  }
}
