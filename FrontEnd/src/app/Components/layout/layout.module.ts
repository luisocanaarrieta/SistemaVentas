import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { DashboardComponent } from './Modulos/dashboard/dashboard.component';
import { UsuarioComponent } from './Modulos/usuario/usuario.component';
import { VentaComponent } from './Modulos/venta/venta.component';
import { ReportesComponent } from './Modulos/reportes/reportes.component';
import { ProductoComponent } from './Modulos/producto/producto.component';
import { HistorialVentaComponent } from './Modulos/historial-venta/historial-venta.component';
import { SharedModule } from 'src/app/Shared/shared/shared.module';
import { ModalUsuarioComponent } from './Modales/modal-usuario/modal-usuario.component';
import { ModalProductoComponent } from './Modales/modal-producto/modal-producto.component';
import { ModalDetalleVentaComponent } from './Modales/modal-detalle-venta/modal-detalle-venta.component';


@NgModule({
  declarations: [
    DashboardComponent,
    UsuarioComponent,
    VentaComponent,
    ReportesComponent,
    ProductoComponent,
    HistorialVentaComponent,
    ModalUsuarioComponent,
    ModalProductoComponent,
    ModalDetalleVentaComponent
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule
  ]
})
export class LayoutModule { }
