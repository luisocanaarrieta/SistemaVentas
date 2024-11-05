import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-detalle-venta',
  templateUrl: './modal-detalle-venta.component.html',
  styleUrls: ['./modal-detalle-venta.component.css']
})
export class ModalDetalleVentaComponent  {

  fechaRegistro:string ="";
  numeroDocumento:string ="";
  tipoPago="";
  total:string="";

  detalleVenta: any[] = []; 
  columnasTabla: string[] = ['sku','producto', 'cantidad', 'precio','total',];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { 
    this.fechaRegistro = data.saleDate!;
    this.numeroDocumento = data.numero;
    this.tipoPago = data.tipoPagoDescripcion;
    this.total = data.saleNet;
    this.detalleVenta = data.ventaDetalles;
  }

}
