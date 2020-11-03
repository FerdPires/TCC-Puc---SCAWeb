import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { ButtonDetailsRendererComponent } from 'src/app/renderer/button-details-renderer.component';

@Component({
  selector: 'app-sensor-area-risco',
  templateUrl: './sensor-area-risco.component.html',
  styleUrls: ['./sensor-area-risco.component.css']
})
export class SensorAreaRiscoComponent implements OnInit {
  frameworkComponents: any;
  rowData: any;
  rowDataClicked = {};
  public columnDefs;

  public nome_barragem = "";

  constructor(
    private authService: AuthService,
    private service: DataService,
    private router: Router
  ) {
    this.frameworkComponents = {
      buttonDetails: ButtonDetailsRendererComponent
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
      { headerName: 'Sensor', field: 'nome_sensor', sortable: true, filter: true, width: 500, resizable: true },
      {
        headerName: 'Status', field: 'status_sensor', sortable: true, filter: true, width: 120, resizable: true,
        valueFormatter: this.formatStatus,
        cellStyle: this.cellStyle,
      },
      {
        headerName: 'Data de Atualização', field: 'data_atualizacao', sortable: true, filter: true, width: 200, resizable: true,
        valueFormatter: this.formatDate,
      },
    ];
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const refreshToken = localStorage.getItem('refresh_token');
      this.nome_barragem = history.state.data.nome_barragem;
      this.service.getAllSensores(history.state.data.id, accessToken)
        .subscribe(
          (data: any) => {
            this.rowData = data;
          }
        );
    });
  }

  details(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/alertas'], { state: { data: this.rowDataClicked, area: this.nome_barragem } });
  }

  formatStatus(params) {
    return params.value == 1 ? "ATIVO" : "INATIVO";
  }

  cellStyle(params) {
    if (params.value == 2) {
      return { backgroundColor: '#ced4da' };
    } else {
      return { backgroundColor: '#aaffaa' };
    }
  }

  formatDate(params) {
    var date = ((new Date(params.value).getDate()).toString().length == 1 ? "0" + new Date(params.value).getDate() : new Date(params.value).getDate()) + "/" +
      ((new Date(params.value).getMonth() + 1).toString().length == 1 ? "0" + (new Date(params.value).getMonth() + 1).toString() : new Date(params.value).getMonth() + 1) + "/" +
      new Date(params.value).getFullYear()
    return date;
  }
}
