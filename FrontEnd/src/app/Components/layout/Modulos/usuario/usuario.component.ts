import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UsuarioService } from 'src/app/Services/Seguridad/usuario.service';
import { SharedService } from 'src/app/Shared/shared.service';
import { ModalUsuarioComponent } from '../../Modales/modal-usuario/modal-usuario.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit, AfterViewInit {

  columnasTabla: string[] = ['nombreCompleto', 'nombreUsuario', 'email', 'telefono', 'rol', 'estado', 'acciones'];
  dataInicio: any[] = [];
  dataListaUsuarios = new MatTableDataSource(this.dataInicio);
  @ViewChild(MatPaginator) paginatorTabla!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _usuarioService: UsuarioService,
    private _sharedService: SharedService,
  ) { }


  obtenerUsuarios() {
    this._usuarioService.obtenerUsuarios().subscribe(r => {
      this.dataListaUsuarios = r;
    })
  }

  ngOnInit(): void {
    this.obtenerUsuarios();
  }

  ngAfterViewInit(): void {
    this.dataListaUsuarios.paginator = this.paginatorTabla;
  }

  aplicarFiltro(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListaUsuarios.filter = filterValue.trim().toLowerCase();
  }

  nuevoUsuario() {
    this.dialog.open(ModalUsuarioComponent, {
      disableClose: true,
    }).afterClosed().subscribe(r => {
      if (r) {
        this.obtenerUsuarios();
      }
    });
  }

  editarUsuario(usuario: any) {
    this.dialog.open(ModalUsuarioComponent, {
      disableClose: true,
      data: usuario
    }).afterClosed().subscribe(r => {
      if (r) {
        this.obtenerUsuarios();
      }
    });
  }

  inactivarUsuario(usuario: any) {
    Swal.fire({
      title: '¿Está seguro de inactivar el usuario?',
      text: usuario.nombreCompleto,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Sí',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this._usuarioService.inactivarUsuario(usuario.id).subscribe(r => {
          if (r) {
            this._sharedService.mensajeAlerta('Usuario inactivado', 'Ok');
            this.obtenerUsuarios();
          } else
            this._sharedService.mensajeAlerta('No se pudo eliminar', 'Error');
        });
      }
    })
  }
}