import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { Operacao } from 'src/app/models/Enums/operacao.enum';
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
  public columnDefs;

  constructor(
    private authService: AuthService,
    private service: DataService,
    private router: Router
  ) {
    this.frameworkComponents = {
      buttonDetails: ButtonDetailsRendererComponent,
      buttonEdit: ButtonEditRendererComponent,
    }

    this.columnDefs = [
      {
        cellRenderer: 'buttonDetails',
        width: 70,
        cellRendererParams: {
          onClick: this.details.bind(this),
          label: 'Detalhes'
        }
      },
      {
        cellRenderer: 'buttonEdit',
        width: 70,
        cellRendererParams: {
          onClick: this.update.bind(this),
          label: 'Editar'
        }
      },
      { headerName: 'Insumo', field: 'descricao_insumo', sortable: true, filter: true, width: 500, resizable: true },
      {
        headerName: 'Status', field: 'status_insumo', sortable: true, filter: true, width: 120, resizable: true,
        valueFormatter: this.formatStatus,
        cellStyle: this.cellStyle,
      },
      {
        headerName: 'Data de Aquisição', field: 'data_aquisicao', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatDate,
      },
    ];
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      this.service.getAllInsumos(accessToken)
        .subscribe(
          (data: any) => {
            this.rowData = data;
          }
        );
    });
  }

  details(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/insumo'], { state: { data: this.rowDataClicked, estadoTela: Operacao.C } });
  }

  update(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/editar-insumo'], { state: { data: this.rowDataClicked, estadoTela: Operacao.E } });
  }

  formatStatus(params) {
    return params.value == 1 ? "ATIVO" : (params.value == 2 ? "INATIVO" : "EM MANUTENÇÃO");
  }

  cellStyle(params) {
    if (params.value == 2) {
      return { backgroundColor: '#ffaaaa' };
    } else if (params.value == 3) {
      return { backgroundColor: '#aaaaff' };
    } else {
      return { backgroundColor: '#aaffaa' };
    }
  }

  formatDate(params) {
    var date = ((new Date(params.value).getDate()).toString().length == 1 ? "0" + new Date(params.value).getDate() : new Date(params.value).getDate()) + "/" +
      ((new Date(params.value).getMonth() + 1).toString().length == 1 ? "0" + new Date(params.value).getMonth() + 1 : new Date(params.value).getMonth() + 1) + "/" +
      new Date(params.value).getFullYear()
    return date;
  }
}
