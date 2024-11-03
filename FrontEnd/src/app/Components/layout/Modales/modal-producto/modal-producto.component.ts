import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CategoriaService } from 'src/app/Services/Mantenimiento/categoria.service';
import { RolService } from 'src/app/Services/Seguridad/rol.service';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';

@Component({
  selector: 'app-modal-producto',
  templateUrl: './modal-producto.component.html',
  styleUrls: ['./modal-producto.component.css']
})
export class ModalProductoComponent implements OnInit {

  formularioProducto: FormGroup
  tituloAccion: string = "Agregar";
  botonAccion: string = "Guardar";

  listaCategorias: any[] = [];
  constructor(
    private modalActual: MatDialogRef<ModalProductoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private _categoriaService: CategoriaService,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
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

  guardarEditarProducto() {
    const producto: any = {
      idUsuario: this.data != null ? this.data.idUsuario : 0,
      nombreCompleto: this.formularioProducto.value.nombreCompleto,
      nombreUsuario: this.formularioProducto.value.nombreUsuario,
      password: this.formularioProducto.value.password,
      idRol: this.formularioProducto.value.idRol,
      email: this.formularioProducto.value.email,
      telefono: this.formularioProducto.value.telefono,
      esActivo: parseInt(this.formularioProducto.value.esActivo),
    }

    if (this.data == null) {
      this._usuarioService.saveUser(producto).subscribe({
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
      this._usuarioService.updateUser(producto).subscribe({
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
