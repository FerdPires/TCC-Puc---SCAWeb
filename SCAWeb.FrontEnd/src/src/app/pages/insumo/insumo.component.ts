import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { Operacao } from 'src/app/models/Enums/operacao.enum';
import { StatusInsumo } from 'src/app/models/Enums/status-insumo.enum';
import { InsumoModel } from 'src/app/models/insumo-model';

@Component({
  selector: 'app-insumo',
  templateUrl: './insumo.component.html',
  styleUrls: ['./insumo.component.css']
})
export class InsumoComponent implements OnInit {
  public estadoTela: Operacao;
  //public id_insumo;

  public form: FormGroup;
  public vm: InsumoModel = new InsumoModel();

  // public insumo = {
  //   Id: '',
  //   descricao_insumo: '',
  //   status_insumo: null,
  //   data_aquisicao: '',
  //   data_atualizacao: '',
  //   qtd_dias_manut_prev: null,
  //   id_tipo_insumo: '',
  //   id_fornec_insumo: '',
  //   user: ''
  // }
  public listTipoInsumo = [];
  public listFornecedor = [];

  public status_insumo = "";

  constructor(
    private fb: FormBuilder,
    private service: DataService,
    private authService: AuthService,
  ) {
    this.form = this.fb.group({
      Id: '',
      descricao_insumo: '',
      status_insumo: '',
      data_aquisicao: '',
      data_atualizacao: '',
      qtd_dias_manut_prev: null,
      id_tipo_insumo: '',
      id_fornec_insumo: ''
    });
  }

  ngOnInit(): void {
    this.estadoTela = Operacao.C;
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const refreshToken = localStorage.getItem('refresh_token');

      this.service.getAllTipoInsumo(accessToken)
        .subscribe(
          (data: any) => {
            data.forEach(element => {
              this.listTipoInsumo.push({ id_tipo_insumo: element.id, descricao_tp_insumo: element.descricao_tp_insumo });
            });
          }
        );
      this.service.getAllFornecedor(accessToken)
        .subscribe(
          (data: any) => {
            data.forEach(element => {
              this.listFornecedor.push({ id_fornec_insumo: element.id, nome_fantasia: element.nome_fantasia });
            });
          }
        );
      if (history.state.data) {
        this.status_insumo = history.state.data.status_insumo == 1 ? "ATIVO" : (history.state.data.status_insumo == 2 ? "INATIVO" : "EM MANUTENÇÃO");
        this.form = this.fb.group({
          Id: history.state.data.id,
          descricao_insumo: history.state.data.descricao_insumo,
          data_aquisicao: new Date(history.state.data.data_aquisicao).toJSON().substring(0, 10),
          qtd_dias_manut_prev: history.state.data.qtd_dias_manut_prev,
          id_tipo_insumo: history.state.data.id_tipo_insumo,
          id_fornec_insumo: history.state.data.id_fornec_insumo
        });
      }
    });
  }
}
