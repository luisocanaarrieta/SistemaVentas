import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CategoriaService } from 'src/app/Services/Mantenimiento/categoria.service';
import { ProductoService } from 'src/app/Services/Mantenimiento/producto.service';
import { RolService } from 'src/app/Services/Seguridad/rol.service';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from 'src/app/Services/Seguridad/login.service';

@Component({
  selector: 'app-modal-producto',
  templateUrl: './modal-producto.component.html',
  styleUrls: ['./modal-producto.component.css']
})
export class ModalProductoComponent implements OnInit {

  formularioProducto: FormGroup
  tituloAccion: string = "Agregar";
  botonAccion: string = "Guardar";

  subUsuario: string ='';
  listaCategorias: any[] = [];
  constructor(
    private modalActual: MatDialogRef<ModalProductoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    private _categoriaService: CategoriaService,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
    private _productoService: ProductoService,
    private _loginService: LoginService,
  ) {

    this.formularioProducto = this.fb.group({
      nombre: ['', Validators.required],
      descripcion: ['', Validators.required],
      precio: ['', Validators.required],
      idCategoria: ['', Validators.required],
      esActivo: ['1', Validators.required],
      stock: ['', Validators.required],
    });

    if (this.data != null) {
      this.tituloAccion = "Editar";
      this.botonAccion = "Actualizar";
    }

    this._categoriaService.obtenerCategorias().subscribe(r => {
      this.listaCategorias = r;
    })

  }
  ngOnInit(): void {
    this.obtenerNombreUsuario();

    if (this.data != null) {
      this.formularioProducto.patchValue({
        nombre: this.data.nombre,
        descripcion: this.data.descripcion,
        precio: this.data.precio,
        idCategoria: this.data.idCategoria,
        stock: this.data.stock,
        esActivo: this.data.esActivo.toString(),
      });
    }
  }

  obtenerNombreUsuario(): void {
    const tokenDecode = this._loginService.getTokenDecode();
    this.subUsuario = tokenDecode.sub

  }


  guardarEditarProducto() {
    const usuario: any = {
      userId: this.data == null ? 0 : this.data.userId,
      userCode: this.formularioProducto.value.codigoTrabajador,
      userName: this.formularioProducto.value.nombreCompleto,
      userUserName: this.formularioProducto.value.usuario,
      userPassword: this.formularioProducto.value.contraseña,
      userRole: this.formularioProducto.value.idRol,
      userMail: this.formularioProducto.value.correo,
      userPhone: this.formularioProducto.value.telefono,
      usuarioCrea: this.subUsuario,
      status: this.formularioProducto.value.esActivo ? true : false
    }

    if (this.data == null) {
      this.spinner.show();
      this._productoService.InsertarProducto(usuario).subscribe({
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
      this._productoService.ActualizarProducto(usuario).subscribe({
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
