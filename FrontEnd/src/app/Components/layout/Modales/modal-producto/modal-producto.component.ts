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

  actualizar: boolean = false;

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
      precio: [0, Validators.required],
      idCategoria: ['', Validators.required],
      esActivo: ['1', Validators.required],
      stock: [0, Validators.required],
    });

    if (this.data != null) {
      this.tituloAccion = "Editar";
      this.botonAccion = "Actualizar";
      this.actualizar = true;
    }

    this._categoriaService.obtenerCategorias().subscribe(r => {
      this.listaCategorias = r;
    })

  }
  ngOnInit(): void {
    this.obtenerNombreUsuario();

    if (this.data != null) {  
      this.formularioProducto.patchValue({
        idProducto : this.data.productId,

        nombre: this.data.productName,
        descripcion: this.data.productSku,
        precio: this.data.productPrice,
        idCategoria: this.data.categoryId,
        stock: this.data.productStock,
        esActivo: this.data.productStatus ? '1' : '0' 
      });
    }
  }

  obtenerNombreUsuario(): void {
    const tokenDecode = this._loginService.getTokenDecode();
    this.subUsuario = tokenDecode.sub

  }


  guardarEditarProducto() {
    const usuario: any = {
      productId: this.data == null ? 0 : this.data.productId,

      productSku: this.formularioProducto.value.descripcion,
      productName: this.formularioProducto.value.nombre,
      categoryId: this.formularioProducto.value.idCategoria,
      productStock: this.formularioProducto.value.stock,
      productPrice: this.formularioProducto.value.precio,
      usuarioCrea: this.subUsuario,
      productStatus: this.formularioProducto.value.esActivo  === '1'
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
          if (r.message  == 'Ok') {
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
          this.spinner.hide();

        }
      }
      );
    }
  }
}
