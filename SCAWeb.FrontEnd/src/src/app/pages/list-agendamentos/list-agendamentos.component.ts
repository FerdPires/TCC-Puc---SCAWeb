import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { Operacao } from 'src/app/models/Enums/operacao.enum';
import { ButtonDetailsRendererComponent } from 'src/app/renderer/button-details-renderer.component';
import { ButtonEditRendererComponent } from 'src/app/renderer/button-edit-renderer.component';

@Component({
  selector: 'app-list-agendamentos',
  templateUrl: './list-agendamentos.component.html',
  styleUrls: ['./list-agendamentos.component.css']
})
export class ListAgendamentosComponent implements OnInit {
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
      { headerName: 'Insumo', field: 'descricao_insumo', sortable: true, filter: true, width: 500, resizable: true },

      {
        headerName: 'Status do Agendamento', field: 'status_insumo', sortable: true, filter: true, width: 120, resizable: true,
        valueFormatter: this.formatStatus,
        cellStyle: this.cellStyle,
      },
      {
        headerName: 'Data do Agendamento', field: 'data_manutencao', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatDate,
      }

    ];
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      this.service.getAllManutAgendadas(accessToken)
        .subscribe(
          (data: any) => {
            this.rowData = data;
          }
        );
    });
  }

  update(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/agenda-manutencao'], { state: { data: this.rowDataClicked, estadoTela: Operacao.E } });
  }

  setButton(param) {
    if (param.data.status_insumo == 1 && param.data.tipo_manutencao == 2) {
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
    return params.value == 1 ? "ABERTO" : "FECHADO";
  }

  cellStyle(params) {
    if (params.value == 1 && new Date(params.data.data_manutencao) < new Date()) {
      return { backgroundColor: '#ffaaaa' };
    } else {
      return { backgroundColor: '#fff' };
    }
  }

  formatDate(params) {
    var date = ((new Date(params.value).getDate()).toString().length == 1 ? "0" + new Date(params.value).getDate() : new Date(params.value).getDate()) + "/" +
      ((new Date(params.value).getMonth() + 1).toString().length == 1 ? "0" + new Date(params.value).getMonth() + 1 : new Date(params.value).getMonth() + 1) + "/" +
      new Date(params.value).getFullYear()
    return date;
  }

}
