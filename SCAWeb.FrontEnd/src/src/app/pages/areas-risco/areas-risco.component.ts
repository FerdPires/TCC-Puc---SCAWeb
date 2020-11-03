import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { ButtonDetailsRendererComponent } from 'src/app/renderer/button-details-renderer.component';

@Component({
  selector: 'app-areas-risco',
  templateUrl: './areas-risco.component.html',
  styleUrls: ['./areas-risco.component.css']
})
export class AreasRiscoComponent implements OnInit {
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
      { headerName: 'Barragem', field: 'nome_barragem', sortable: true, filter: true, width: 500, resizable: true },
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
      this.service.getAllAreasRisco(accessToken)
        .subscribe(
          (data: any) => {
            this.rowData = data;
          }
        );
    });
  }

  details(e) {
    this.rowDataClicked = e.rowData;
    this.router.navigate(['/sensores-risco'], { state: { data: this.rowDataClicked } });
  }

  formatDate(params) {
    var date = ((new Date(params.value).getDate()).toString().length == 1 ? "0" + new Date(params.value).getDate() : new Date(params.value).getDate()) + "/" +
      ((new Date(params.value).getMonth() + 1).toString().length == 1 ? "0" + (new Date(params.value).getMonth() + 1).toString() : new Date(params.value).getMonth() + 1) + "/" +
      new Date(params.value).getFullYear()
    return date;
  }
}
