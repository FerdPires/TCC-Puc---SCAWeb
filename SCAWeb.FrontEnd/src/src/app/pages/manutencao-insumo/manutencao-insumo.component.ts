import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr/public_api';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { Operacao } from 'src/app/models/Enums/operacao.enum';
import { ManutencaoInsumoModel } from 'src/app/models/manutencao-insumo-model';
import * as moment from 'moment';

@Component({
  selector: 'app-manutencao-insumo',
  templateUrl: './manutencao-insumo.component.html',
  styleUrls: ['./manutencao-insumo.component.css']
})
export class ManutencaoInsumoComponent implements OnInit {
  public form: FormGroup;
  public vm: ManutencaoInsumoModel = new ManutencaoInsumoModel();
  public estadoTela: Operacao;

  public listInsumosAgendadosAteHoje = [];
  public user: any = {
    name: ""

  };

  constructor(
    private fb: FormBuilder,
    private service: DataService,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
  ) {
    this.form = this.fb.group({
      tipo_manutencao: null,
      descricao_manutencao: '',
      status_manutencao: null,
      data_inicio: moment(new Date()).format("DD/MM/YYYY"),
      data_fim: '',
      id_insumo: '',
      user: ''
    });
  }

  ngOnInit(): void {
    this.estadoTela = Operacao.I;
  }

  buscaItensManutencao(data: any) {
    this.authService.user$.subscribe(x => {
      debugger
      const accessToken = localStorage.getItem('access_token');
      const token = x;
      if (data == 1) {
        this.service.GetAllPreventiva(accessToken)
          .subscribe(
            (data: any) => {
              this.listInsumosAgendadosAteHoje = data;
            }
          );
      } else if (data == 2) {
        this.service.GetAllCorretiva(accessToken)
          .subscribe(
            (data: any) => {
              this.listInsumosAgendadosAteHoje = data;
            }
          );
      }
    });
  }

  submit() {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const token = x;
      this.vm.Registros.tipo_manutencao = this.form.value.tipo_manutencao;
      this.vm.Registros.descricao_manutencao = this.form.value.descricao_manutencao;
      //this.vm.Registros.status_manutencao = this.form.value.qtd_dias_manut_prev;
      this.vm.Registros.data_inicio = this.form.value.data_inicio;
      //this.vm.Registros.data_fim = this.form.value.id_fornec_insumo;
      this.vm.Registros.id_insumo = this.form.value.id_insumo;

      this.service.postManutencao(this.vm.Registros, accessToken)
        .subscribe((res: any) => {
          if (res.success) {
            this.toastr.success(res.message);
          } else {
            this.toastr.error(res.message);
          }
          this.router.navigateByUrl("/");
        });
    });
  }

  finalizarManutencao() {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const token = x;

      this.vm.Registros.descricao_manutencao = this.form.value.descricao_manutencao;
      this.vm.Registros.tipo_manutencao = this.form.value.tipo_manutencao;
      this.vm.Registros.status_manutencao = 2;
      this.vm.Registros.id_insumo = this.form.value.id_insumo;

      if (this.form.value.tipo_manutencao == 1) {
        this.service.putManutCorretiva(this.vm.Registros, accessToken)
          .subscribe((res: any) => {
            if (res.success) {
              this.toastr.success(res.message);
            } else {
              this.toastr.error(res.message);
            }
            this.router.navigateByUrl("/");
          });
      } else if (this.form.value.tipo_manutencao == 2) {
        this.service.putManutPreventiva(this.vm.Registros, accessToken)
          .subscribe((res: any) => {
            if (res.success) {
              this.toastr.success(res.message);
            } else {
              this.toastr.error(res.message);
            }
            this.router.navigateByUrl("/");
          });
      }
    });
  }

}
