import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { Operacao } from 'src/app/models/Enums/operacao.enum';
import { ButtonEditRendererComponent } from 'src/app/renderer/button-edit-renderer.component';

@Component({
  selector: 'app-list-manutencao',
  templateUrl: './list-manutencao.component.html',
  styleUrls: ['./list-manutencao.component.css']
})
export class ListManutencaoComponent implements OnInit {
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
      buttonEdit: ButtonEditRendererComponent,
    }

    this.columnDefs = [
      {
        cellRendererSelector: this.setButton,
        width: 70,
        cellRendererParams: {
          onClick: this.update.bind(this),
          label: 'Editar'
        }
      },
      {
        headerName: 'Tipo da Manutenção', field: 'tipo_manutencao', sortable: true, filter: true, width: 120, resizable: true,
        valueFormatter: this.formatManut
      },
      { headerName: 'Descrição', field: 'descricao_manutencao', sortable: true, filter: true, width: 500, resizable: true },
      { headerName: 'Insumo', field: 'descricao_insumo', sortable: true, filter: true, width: 500, resizable: true },
      {
        headerName: 'Status da Manutenção', field: 'status_manutencao', sortable: true, filter: true, width: 120, resizable: true,
        valueFormatter: this.formatStatus
      },
      {
        headerName: 'Data Início', field: 'data_inicio', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatDate,
      },
      {
        headerName: 'Data Fim', field: 'data_fim', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatDate,
      }
    ];
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      this.service.getAllManutencoes(accessToken)
        .subscribe(
          (data: any) => {
            this.rowData = data;
          }
        );
    });
  }

  update(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/manutencao'], { state: { data: this.rowDataClicked, estadoTela: Operacao.E } });
  }

  setButton(param) {
    if (param.data.status_manutencao == 1) {
      var buttonEdit = {
        component: 'buttonEdit'
      };
      return buttonEdit;
    }
  }

  formatManut(params) {
    return params.value == 1 ? "PREVENTIVA" : "CORRETIVA";
  }

  formatStatus(params) {
    return params.value == 1 ? "INICIADA" : "CONCLUÍDA";
  }

  formatDate(params) {
    if (params.value) {
      var date = ((new Date(params.value).getDate()).toString().length == 1 ? "0" + new Date(params.value).getDate() : new Date(params.value).getDate()) + "/" +
        ((new Date(params.value).getMonth() + 1).toString().length == 1 ? "0" + new Date(params.value).getMonth() + 1 : new Date(params.value).getMonth() + 1) + "/" +
        new Date(params.value).getFullYear()

      return date;
    }
  }
}
