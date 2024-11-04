import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RolService } from 'src/app/Services/Seguridad/rol.service';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';
import { LoginService } from 'src/app/Services/Seguridad/login.service';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-modal-usuario',
  templateUrl: './modal-usuario.component.html',
  styleUrls: ['./modal-usuario.component.css']
})
export class ModalUsuarioComponent implements OnInit {

  formularioUsuario: FormGroup
  tituloAccion: string = "Agregar";
  botonAccion: string = "Guardar";

  subUsuario: string ='';
  tokenDecode: any;
  listaRoles: any[] = [];

  constructor(
    private modalActual: MatDialogRef<ModalUsuarioComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private _rolService: RolService,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
    private _loginService: LoginService,
    private spinner: NgxSpinnerService,

  ) {

    this.formularioUsuario = this.fb.group({
      nombreCompleto: ['', Validators.required],
      idRol: [1, Validators.required],
      correo: ['', Validators.required],
      telefono: ['', Validators.required], 
      usuario: ['', Validators.required],
      contraseña: ['', Validators.required],
      codigoTrabajador : ['', Validators.required],
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
    this.obtenerNombreUsuario();

    if (this.data != null) {
      this.formularioUsuario.patchValue({
        idUsuario: this.data.userId,

        nombreCompleto: this.data.userName,
        idRol: this.data.rolId,
        correo: this.data.userEmail,
        telefono: this.data.userPhone,
        usuario: this.data.userUsername,
        contraseña: this.data.userPassword,    
        codigoTrabajador: this.data.userCode,      
        esActivo: this.data.userStatus ? '1' : '0' 
      });
    }
  }

  obtenerNombreUsuario(): void {
    const tokenDecode = this._loginService.getTokenDecode();
    this.subUsuario = tokenDecode.sub

  }

  guardarEditarUsuario() {
    const usuario: any = {
      userId: this.data == null ? 0 : this.data.userId,
      userCode: this.formularioUsuario.value.codigoTrabajador,
      userName: this.formularioUsuario.value.nombreCompleto,
      userUserName: this.formularioUsuario.value.usuario,
      userPassword: this.formularioUsuario.value.contraseña,
      userRole: this.formularioUsuario.value.idRol,
      userMail: this.formularioUsuario.value.correo,
      userPhone: this.formularioUsuario.value.telefono,
      usuarioCrea: this.subUsuario,
      status: this.formularioUsuario.value.esActivo ? true : false
    }

    if (this.data == null) {
      this.spinner.show();
      this._usuarioService.saveUser(usuario).subscribe({
        next: (r) => {
          if (r.message == 'OK') {
            this._sharedService.mensajeAlerta(r.message, "Éxito");
            this.modalActual.close("true");

          } else {
            this._sharedService.mensajeAlerta("Error al guardar el usuario", "Error");
          }
          this.spinner.hide();
        },
        error: (err) => {
          const errorMessage = err.error?.message || "Error al guardar el usuario";
          this._sharedService.mensajeAlerta(errorMessage, "Error");
          this.spinner.hide();
        }
      });
    }
    else {
      this.spinner.show();
      this._usuarioService.ActualizarUsuario(usuario).subscribe({
        next: (r) => {
          if (r.message  == 'OK') {
            this._sharedService.mensajeAlerta(r.message, "Éxito");
            this.modalActual.close("true");    
          }
          else {
            this._sharedService.mensajeAlerta("Error al actualizar el usuarioA", "Error");
          }
          this.spinner.hide();
        },
        error: (  ) => {
          this._sharedService.mensajeAlerta("Error al actualizar el usuarioB", "Error");
        }
      }
      );
    }
  }
}
