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
  public listInsumos = [];

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
        headerName: 'Tipo da Manutenção', field: 'tipo_manutencao', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatManut
      },
      { headerName: 'Insumo', field: 'descricao_insumo', sortable: true, filter: true, width: 500, resizable: true },
      {
        headerName: 'Status', field: 'status_agenda', sortable: true, filter: true, width: 120, resizable: true,
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
            this.service.getAllInsumos(accessToken)
              .subscribe(
                (insumos: any) => {
                  this.rowData = [];
                  for (let x = 0; x < data.length; x++) {
                    const i = data[x];
                    for (let y = 0; y < insumos.length; y++) {
                      const j = insumos[y];
                      if (i.id_insumo == j.id) {
                        this.rowData.push({
                          Id: i.id,
                          tipo_manutencao: i.tipo_manutencao,
                          status_agenda: i.status_agenda,
                          data_manutencao: i.data_manutencao,
                          data_atualizacao: i.data_atualizacao,
                          id_insumo: i.id_insumo,
                          descricao_insumo: j.descricao_insumo
                        });
                        break;
                      }
                    }
                  }
                }
              );
          }
        );

    });
  }

  update(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/agenda-manutencao'], { state: { data: this.rowDataClicked, estadoTela: Operacao.E } });
  }

  setButton(param) {
    if (param.data.status_agenda == 1 && param.data.tipo_manutencao == 2) {
      var buttonEdit = {
        component: 'buttonEdit'
      };
      return buttonEdit;
    }
  }

  formatManut(params) {
    return params.data.tipo_manutencao == 1 ? "PREVENTIVA" : "CORRETIVA";
  }

  formatStatus(params) {
    return params.data.status_agenda == 1 ? "ABERTO" : "FECHADO";
  }

  cellStyle(params) {
    if (params.data.status_agenda == 1 && new Date(params.data.data_manutencao) < new Date()) {
      return { backgroundColor: '#ffaaaa' };
    } else {
      return { backgroundColor: '#fff' };
    }
  }

  formatDate(params) {
    var date = ((new Date(params.data.data_manutencao).getDate()).toString().length == 1 ? "0" + new Date(params.data.data_manutencao).getDate() : new Date(params.data.data_manutencao).getDate()) + "/" +
      ((new Date(params.data.data_manutencao).getMonth() + 1).toString().length == 1 ? "0" + new Date(params.data.data_manutencao).getMonth() + 1 : new Date(params.data.data_manutencao).getMonth() + 1) + "/" +
      new Date(params.data.data_manutencao).getFullYear()
    return date;
  }

}
