import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';
import { ModalUsuarioComponent } from '../../Modales/modal-usuario/modal-usuario.component';
import { NgxSpinnerService } from 'ngx-spinner';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit, AfterViewInit {

  columnasTabla: string[] = ['nombreCompleto', 'nombreUsuario', 'email', 'telefono', 'rol', 'estado', 'acciones'];
  dataInicio: any[] = [];

  public matDataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.ListarUsuarios();
  }

  ngAfterViewInit(): void {
    this.matDataSource.paginator = this.paginator;
  }

  ListarUsuarios() {
    this._usuarioService.ListarUsuarios().subscribe(r => {
      this.matDataSource.data = r;
    })
  }

  nuevoUsuario() {
    this.dialog.open(ModalUsuarioComponent, {
      disableClose: true,
    }).afterClosed().subscribe(r => {
      if (r) {
        this.ListarUsuarios();
      }
    });
  }

  editarUsuario(usuario: any) {
    this.dialog.open(ModalUsuarioComponent, {
      disableClose: true,
      data: usuario
    }).afterClosed().subscribe(r => {
      if (r) {
        this.ListarUsuarios();
      }
    });
  }

  inactivarUsuario(usuario: any) {
    Swal.fire({
      title: '¿Está seguro de inactivar el usuario?',
      text: usuario.userName,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Sí',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.spinner.show();
        this._usuarioService.EliminarUsuario(usuario.userId).subscribe(r => {
          if (r) {
            this._sharedService.mensajeAlerta('Usuario inactivado', 'Ok');
            this.ListarUsuarios();
            this.spinner.hide();
          } else {
            this._sharedService.mensajeAlerta('No se pudo eliminar', 'Error');
          }
          this.spinner.hide();
        });
      }
    })
  }

  aplicarFiltro(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.matDataSource.filter = filterValue.trim().toLowerCase();
  }

}