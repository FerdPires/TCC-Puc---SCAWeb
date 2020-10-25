import { Component, OnInit, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular/lib/ag-grid-angular.component';
import { ButtonDeleteRendererComponent } from 'src/app/renderer/button-delete-renderer.component';

@Component({
  selector: 'app-list-insumos',
  templateUrl: './list-insumos.component.html',
  styleUrls: ['./list-insumos.component.css']
})
export class ListInsumosComponent implements OnInit {
  frameworkComponents: any;
  rowData: any;
  rowDataClicked = {};

  constructor() {
    this.frameworkComponents = {
      buttonDelete: ButtonDeleteRendererComponent,
      buttonDetails: ButtonDeleteRendererComponent,
      buttonEdit: ButtonDeleteRendererComponent,
    }
  }

  columnDefs = [
    {
      headerName: 'Pesquisar',
      cellRenderer: 'buttonDetails',
      cellRendererParams: {
        onClick: this.onBtnClick.bind(this),
        label: 'Click'
      }
    },
    { field: 'descricao_insumo', sortable: true, filter: true },
    { field: 'status_insumo', sortable: true, filter: true },
    { field: 'data_aquisicao', sortable: true, filter: true },
    {
      headerName: 'Editar',
      cellRenderer: 'buttonEdit',
      cellRendererParams: {
        onClick: this.onBtnClick.bind(this),
        label: 'Click'
      }
    },
    {
      headerName: 'Excluir',
      cellRenderer: 'buttonDelete',
      cellRendererParams: {
        onClick: this.onBtnClick.bind(this),
        label: 'Click'
      }
    }
  ];



  ngOnInit(): void {
  }

  onBtnClick(e) {
    this.rowDataClicked = e.rowData;
  }
}
