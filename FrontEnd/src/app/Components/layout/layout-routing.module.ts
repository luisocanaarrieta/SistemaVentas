import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './layout.component';
import { DashboardComponent } from './Modulos/dashboard/dashboard.component';
import { UsuarioComponent } from './Modulos/usuario/usuario.component';
import { VentaComponent } from './Modulos/venta/venta.component';
import { ReportesComponent } from './Modulos/reportes/reportes.component';
import { ProductoComponent } from './Modulos/producto/producto.component';
import { HistorialVentaComponent } from './Modulos/historial-venta/historial-venta.component';

const routes: Routes = [{
  path: '',
  component: LayoutComponent, children: [
    {path: 'dashboard', component: DashboardComponent},  
    {path: 'usuarios', component: UsuarioComponent}, 
    {path: 'venta', component: VentaComponent}, 
    {path: 'historial-venta', component: HistorialVentaComponent}, 
    {path: 'reportes', component: ReportesComponent},  
    {path: 'producto', component: ProductoComponent},  
  ]
}];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
