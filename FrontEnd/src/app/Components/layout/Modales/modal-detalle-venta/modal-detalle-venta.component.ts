import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { VentasService } from 'src/app/Services/Venta/ventas.service';
import { SharedService } from 'src/app/Shared/shared.service';

@Component({
  selector: 'app-modal-detalle-venta',
  templateUrl: './modal-detalle-venta.component.html',
  styleUrls: ['./modal-detalle-venta.component.css']
})
export class ModalDetalleVentaComponent implements OnInit {

  formularioListaEstados: FormGroup

  listaEstados: any[] = [];

  fechaRegistro: string = "";
  numeroDocumento: string = "";
  tipoPago = "";
  total: string = "";

  detalleVenta: any[] = [];
  columnasTabla: string[] = ['sku', 'producto', 'cantidad', 'precio', 'total',];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private _ventasService: VentasService,
    private modalActual: MatDialogRef<ModalDetalleVentaComponent>,
    private _sharedService: SharedService,

  ) {

    this.fechaRegistro = data.saleDate!;
    this.numeroDocumento = data.numero;
    this.tipoPago = data.tipoPagoDescripcion;
    this.total = data.saleNet;
    this.detalleVenta = data.ventaDetalles;

    this.formularioListaEstados = this.fb.group({
      estadoReparto: [0, Validators.required],
    })


  }
  ngOnInit(): void {
    this.formularioListaEstados.patchValue({
      estadoReparto: this.data.statusOrderId,
    });

    this.ListarEstadosReparto();
  }


  ListarEstadosReparto() {
    this._ventasService.ListarEstadosReparto().subscribe(r => {
      this.listaEstados = r;
    })
  }

  getFormData(): any {
    const formData = this.formularioListaEstados.getRawValue();

    const data = {
      saleId: this.data.saleId,
      statusVentaId: formData.estadoReparto,
    }
    return data;
  }

  CambiarEstadoVenta() {
    const formData = this.getFormData();
    this._ventasService.CambiarEstadoVenta(formData).subscribe({
      next: r => {
        if (r.message == 'OK') {
          this._sharedService.mensajeAlerta(r.message, "Éxito");
          this.modalActual.close("true");
        } else {
          this._sharedService.mensajeAlerta(r.message, "Error");
        }
      },
      error: err => {
        this._sharedService.mensajeAlerta('Error en la solicitud', "Error");
      }
    }); 
  }
  
}
