import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductoService } from 'src/app/Services/Mantenimiento/producto.service';
import { LoginService } from 'src/app/Services/Seguridad/login.service';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { VentasService } from 'src/app/Services/Venta/ventas.service';
import { SharedService } from 'src/app/Shared/shared.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-venta',
  templateUrl: './venta.component.html',
  styleUrls: ['./venta.component.css']
})
export class VentaComponent implements OnInit {

  listaProductos: any[] = [];
  listaProductosFiltro: any[] = [];
  listaProductosParaVenta: any[] = [];

  bloquearBotonRegistrar: boolean = false;
  productoSeleccionado!: any;
  tipoPagoDefault: string = 'Efectivo';
  totalPagar: number = 0;

  formularioProductoVenta: FormGroup;
  columnasTabla: string[] = ['producto', 'cantidad', 'precio', 'total', 'acciones'];

  public matDataSource = new MatTableDataSource<any>();

  productosPorFiltro(value: any) {
    const valorBuscado = typeof value === 'string' ? value.toLocaleLowerCase() : value.productName.toLocaleLowerCase();
    return this.listaProductos.filter(item => item.productName.toLocaleLowerCase().includes(valorBuscado));
  }

  constructor(
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
    private _productoService: ProductoService,
    private _loginService: LoginService,
    private _ventasService: VentasService,
  ) {
    this.formularioProductoVenta = this.fb.group({
      producto: ['', Validators.required],
      cantidad: ['', Validators.required],
    });

    this._productoService.ListarProductos().subscribe(r => {
      const lista = r as any[];
      this.listaProductos = lista.filter(p => p.productStock > 0);
    })

    this.formularioProductoVenta.get('producto')?.valueChanges.subscribe(value => {
      this.listaProductosFiltro = this.productosPorFiltro(value);
    })
  }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  mostrarProducto(producto: any): string {
    return producto.productName;
  }

  productoParaVenta(event: any) {
    this.productoSeleccionado = event.option.value;
    console.log(this.productoSeleccionado);
  }

  agregarProductoVenta() {
    const _cantidad: number = this.formularioProductoVenta.value.cantidad;
    const _precio: number = parseFloat(this.productoSeleccionado.productPrice);
    const _total: number = _cantidad * _precio;
    this.totalPagar += _total;

    this.listaProductosParaVenta.push({
      productId: this.productoSeleccionado.productId,
      productName: this.productoSeleccionado.productName,
      cantidad: _cantidad,
      precioTexto: String(_precio.toFixed(2)),
      totalTexto: String(_total.toFixed(2)),
    })

    this.matDataSource = new MatTableDataSource(this.listaProductosParaVenta);

    this.formularioProductoVenta.patchValue({
      producto: '',
      cantidad: '',

    });
  }

  eliminarProducto(detalleVenta: any) {
    this.totalPagar -= parseFloat(detalleVenta.totalTexto);
    this.listaProductosParaVenta = this.listaProductosParaVenta.filter(p => p.productId !== detalleVenta.productId);
    this.matDataSource = new MatTableDataSource(this.listaProductosParaVenta);
  }

  registrarVenta() {

    if (this.listaProductosParaVenta.length === 0) {
      this._sharedService.mensajeAlerta('Debe seleccionar al menos un producto', 'Ok');
    } else {
      this.bloquearBotonRegistrar = true;

      const venta: any = {
        tipoPago: this.tipoPagoDefault,
        totalTexto: String(this.totalPagar.toFixed(2)),
        detalleVenta: this.listaProductosParaVenta
      }

      this.spinner.show();
      this._ventasService.RegistrarVenta(venta).subscribe({
        next: (r) => {
          if (r.message === 'OK') {
            this.totalPagar = 0.00;
            this._sharedService.mensajeAlerta(r.message, 'Éxito');
            this.limpiarFormulario();

            Swal.fire({
              icon: 'success',
              title: 'Venta Registrada',
              text: 'Numero de venta: ' + r.numeroVenta,
              confirmButtonColor: '#3085d6',
              confirmButtonText: 'Sí',
              showCancelButton: true,
              cancelButtonColor: '#d33',
              cancelButtonText: 'No'
            })
            
          } else {
            this._sharedService.mensajeAlerta('Error al registrar la venta', 'Error');
          }
          this.spinner.hide();
          this.bloquearBotonRegistrar = false;
        },
        error: (err) => {
          const errorMessage = err.error?.message || 'Error al guardar la venta';
          this._sharedService.mensajeAlerta(errorMessage, 'Error');
          this.spinner.hide();
          this.bloquearBotonRegistrar = false;
        }
      });
    }
  }

  limpiarFormulario() {
    this.totalPagar = 0.00;
    this.listaProductosParaVenta = [];
    this.matDataSource = new MatTableDataSource(this.listaProductosParaVenta);
  }

}
