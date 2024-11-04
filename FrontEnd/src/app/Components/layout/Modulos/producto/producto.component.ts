import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductoService } from 'src/app/Services/Mantenimiento/producto.service';

import Swal from 'sweetalert2';
import { ModalProductoComponent } from '../../Modales/modal-producto/modal-producto.component';

@Component({
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css']
})
export class ProductoComponent implements OnInit, AfterViewInit {

  columnasTabla: string[] = ['productSku', 'productName', 'categoryName', 'productStock', 'productPrice', 'productStatus', 'acciones'];
  dataInicio: any[] = [];

  public matDataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;


  constructor(
    private dialog: MatDialog,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
    private _productoService: ProductoService,
    private spinner: NgxSpinnerService

  ) { }

  ngOnInit(): void {
    this.ListarProductos();
  }


  ngAfterViewInit(): void {
    this.matDataSource.paginator = this.paginator;

  }

  ListarProductos() {
    this._productoService.ListarProductos().subscribe(r => {
      this.matDataSource.data = r;
    })
  }

  nuevoProducto() {
    this.dialog.open(ModalProductoComponent, {
      disableClose: true,
    }).afterClosed().subscribe(r => {
      if (r) {
        this.ListarProductos();
      }
    });
  }

  editarProducto(producto: any) {
    this.dialog.open(ModalProductoComponent, {
      disableClose: true,
      data: producto
    }).afterClosed().subscribe(r => {
      if (r) {
        this.ListarProductos();
      }
    });
  }

  inactivarProducto(producto: any) {
    Swal.fire({
      title: '¿Está seguro de inactivar el producto?',
      text: producto.productName,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Sí',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.spinner.show();
        this._productoService.EliminarProducto(producto.userId).subscribe(r => {
          if (r) {
            this._sharedService.mensajeAlerta('Producto inactivado', 'Ok');
            this.ListarProductos();
            this.spinner.hide();
          } else
            this._sharedService.mensajeAlerta('No se pudo eliminar', 'Error');
        });
      }
    })
  }

  aplicarFiltro(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.matDataSource.filter = filterValue.trim().toLowerCase();
  }

}
