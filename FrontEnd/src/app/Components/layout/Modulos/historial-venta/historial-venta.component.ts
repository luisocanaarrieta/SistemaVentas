import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SharedService } from 'src/app/Shared/shared.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as moment from 'moment';
import { VentasService } from 'src/app/Services/Venta/ventas.service';
import { ModalDetalleVentaComponent } from '../../Modales/modal-detalle-venta/modal-detalle-venta.component';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-historial-venta',
  templateUrl: './historial-venta.component.html',
  styleUrls: ['./historial-venta.component.css']
})
export class HistorialVentaComponent implements AfterViewInit {
  filaResaltada: any = undefined;

  formularioBusqueda: FormGroup;
  opcionesBusqueda: any[] = [
    { value: "0", descripcion: "Por fechas" },
    { value: "1", descripcion: "Numero Venta" },
  ];


  constructor(
    private dialog: MatDialog,
    private _ventaService: VentasService,
    private _sharedService: SharedService,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder
  ) {
    this.formularioBusqueda = this.fb.group({
      buscarPor: ['0',],
      numero: [''],
      fechaInicio: [new Date()],
      fechaFin: [new Date()],

    });

    this.formularioBusqueda.get('buscarPor')?.valueChanges.subscribe(r => {
      this.formularioBusqueda.patchValue({
        numero: '',
        fechaInicio: '',
        fechaFin: ''
      });
    });
  }

  columnasTabla: string[] = ['fechaRegistro', 'numeroDocumento', 'tipoPago', 'estado', 'total'];
  dataInicio: any[] = [];
  public matDataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit(): void {
    this.matDataSource.paginator = this.paginator;
  }

  aplicarFiltro(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.matDataSource.filter = filterValue.trim().toLowerCase();
  }

  buscarVentas() {
    let fechaInicio:string ="";
    let fechaFin:string ="";

    if(this.formularioBusqueda.value.buscarPor === '0'){
      fechaInicio = formatDate(this.formularioBusqueda.value.fechaInicio ?? new Date(), 'yyyy-MM-dd', 'en-US'),
      fechaFin = formatDate(this.formularioBusqueda.value.fechaFin ?? new Date(), 'yyyy-MM-dd', 'en-US')
      
      if(fechaInicio==="Invalid date" || fechaFin==="Invalid date"){
        this._sharedService.mensajeAlerta('Las fechas ingresadas no son validas', 'Oops');
        return;
      }
    }

    const venta : any= {
      filtroBuscar : this.formularioBusqueda.value.buscarPor,
      numero : this.formularioBusqueda.value.numero,
      fechaInicio,
      fechaFin
    }

    this.spinner.show();
    this._ventaService.ObtenerVentasConDetalle(
      venta
    ).subscribe({
      next: (r) => {
        if (r.message === 'OK') {
          this.matDataSource.data = r.resultado;
        }else{
          this._sharedService.mensajeAlerta("No se encontraron datos", 'Oops');
        }
        this.spinner.hide();

      },
      error: (  ) => {
        this._sharedService.mensajeAlerta("Error al traer la lista", "Error");
        this.spinner.hide();

      }

    });
    
  }

  verDetalleVenta(venta: any) {
    this.dialog.open(ModalDetalleVentaComponent, {
      disableClose: true,
      data: venta,
      width: '800px'
    }).afterClosed().subscribe(r => {
      if (r) {
        this.buscarVentas();
      }
    });
  }

  resaltarFila(row: any): void {
    this.filaResaltada = row;
  }

  quitarResaltado(): void {
    this.filaResaltada = undefined;
  }
}
