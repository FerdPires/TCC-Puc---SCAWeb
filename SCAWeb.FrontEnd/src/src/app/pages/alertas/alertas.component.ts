import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';

@Component({
  selector: 'app-alertas',
  templateUrl: './alertas.component.html',
  styleUrls: ['./alertas.component.css']
})
export class AlertasComponent implements OnInit {
  rowData: any;
  rowDataClicked = {};
  public columnDefs;

  public nome_barragem = "";
  public nome_sensor = "";

  constructor(
    private authService: AuthService,
    private service: DataService
  ) {
    this.columnDefs = [
      {
        headerName: 'Tipo do Alerta', field: 'tipo_aletra', sortable: true, filter: true, width: 160, resizable: true,
        valueFormatter: this.formatStatus,
        cellStyle: this.cellStyle,
      },
      { headerName: 'Descrição', field: 'descricao_alerta', sortable: true, filter: true, width: 500, resizable: true },
      {
        headerName: 'Data', field: 'data_atualizacao', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatDate,
      },
    ];
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const refreshToken = localStorage.getItem('refresh_token');
      this.nome_barragem = history.state.area;
      this.nome_sensor = history.state.data.nome_sensor;
      this.service.getAllAlertas(history.state.data.id, accessToken)
        .subscribe(
          (data: any) => {
            this.rowData = data;
          }
        );
    });
  }

  formatStatus(params) {
    return params.value == 1 ? "ALTO RISCO" : "ATENÇÃO";
  }

  cellStyle(params) {
    if (params.value == 2) {
      return { backgroundColor: '#ffd640' };
    } else {
      return { backgroundColor: '#ffaaaa' };
    }
  }

  formatDate(params) {
    var date = ((new Date(params.value).getDate()).toString().length == 1 ? "0" + new Date(params.value).getDate() : new Date(params.value).getDate()) + "/" +
      ((new Date(params.value).getMonth() + 1).toString().length == 1 ? "0" + new Date(params.value).getMonth() + 1 : new Date(params.value).getMonth() + 1) + "/" +
      new Date(params.value).getFullYear()
    return date;
  }
}
