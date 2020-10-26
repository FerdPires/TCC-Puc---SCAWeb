import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { ButtonDeleteRendererComponent } from 'src/app/renderer/button-delete-renderer.component';
import { ButtonDetailsRendererComponent } from 'src/app/renderer/button-details-renderer.component';
import { ButtonEditRendererComponent } from 'src/app/renderer/button-edit-renderer.component';

@Component({
  selector: 'app-list-insumos',
  templateUrl: './list-insumos.component.html',
  styleUrls: ['./list-insumos.component.css']
})
export class ListInsumosComponent implements OnInit {
  frameworkComponents: any;
  rowData: any;
  rowDataClicked = {};

  constructor(
    private authService: AuthService,
    private service: DataService,
  ) {
    this.frameworkComponents = {
      buttonDelete: ButtonDeleteRendererComponent,
      buttonDetails: ButtonDetailsRendererComponent,
      buttonEdit: ButtonEditRendererComponent,
    }
  }

  columnDefs = [
    {
      headerName: 'Pesquisar',
      cellRenderer: 'buttonDetails',
      cellRendererParams: {
        onClick: this.details.bind(this),
        label: 'Detalhes'
      }
    },
    { field: 'descricao_insumo', sortable: true, filter: true },
    { field: 'status_insumo', sortable: true, filter: true },
    { field: 'data_aquisicao', sortable: true, filter: true },
    {
      headerName: 'Editar',
      cellRenderer: 'buttonEdit',
      cellRendererParams: {
        onClick: this.update.bind(this),
        label: 'Editar'
      }
    },
    {
      headerName: 'Excluir',
      cellRenderer: 'buttonDelete',
      cellRendererParams: {
        onClick: this.delete.bind(this),
        label: 'Excluir'
      }
    }
  ];



  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      this.service.getAllInsumos(accessToken)
        .subscribe(
          (data: any) => {
            debugger
            this.rowData = data;
          }
        );
    });
  }

  details(e) {
    this.rowDataClicked = e.rowData;
  }

  update(e) {
    this.rowDataClicked = e.rowData;
  }

  delete(e) {
    this.rowDataClicked = e.rowData;
  }
}
