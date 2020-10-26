import { Component, OnInit } from '@angular/core';
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
  public form: FormGroup;
  public vm: InsumoModel = new InsumoModel();
  public estadoTela: Operacao;
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
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
  ) {
    this.form = this.fb.group({
      descricao_insumo: '',
      status_insumo: '',
      data_aquisicao: '',
      data_atualizacao: '',
      qtd_dias_manut_prev: null,
      id_tipo_insumo: '',
      id_fornec_insumo: '',
      user: ''
    });
  }

  ngOnInit(): void {
    // this.estadoTela = Operacao.I;

    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const refreshToken = localStorage.getItem('refresh_token');
      this.status_insumo = "ATIVO";
      this.service.getAllTipoInsumo(accessToken)
        .subscribe(
          (data: any) => {
            debugger
            this.listTipoInsumo = data;
          }
        );
      this.service.getAllFornecedor(accessToken)
        .subscribe(
          (data: any) => {
            this.listFornecedor = data;
          }
        );
    });
  }

  salvar() {
    if (this.estadoTela == Operacao.I) {
      this.authService.user$.subscribe(x => {
        debugger
        const accessToken = localStorage.getItem('access_token');
        const token = x;
        this.vm.Registros.descricao_insumo = this.form.value.descricao_insumo;
        this.vm.Registros.data_aquisicao = new Date(this.form.value.data_aquisicao).toJSON();
        this.vm.Registros.qtd_dias_manut_prev = this.form.value.qtd_dias_manut_prev;
        this.vm.Registros.id_tipo_insumo = this.form.value.id_tipo_insumo;
        this.vm.Registros.id_fornec_insumo = this.form.value.id_fornec_insumo;

        this.service.postInsumo(this.vm.Registros, accessToken)
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
    else if (this.estadoTela == Operacao.E) {
      this.update();
    }
  }

  update() {
    this.authService.user$.subscribe(token => {
      debugger
      this.vm.Registros.Id = this.form.value.Id;
      this.vm.Registros.descricao_insumo = this.form.value.descricao_insumo;
      this.vm.Registros.status_insumo = this.form.value.status_insumo == "ATIVO" ? StatusInsumo.Ativo :
        (this.form.value.status_insumo == "INATIVO" ? StatusInsumo.Inativo : StatusInsumo.Manutencao);
      this.vm.Registros.qtd_dias_manut_prev = this.form.value.qtd_dias_manut_prev;

      this.service.putInsumo(this.vm.Registros, token)
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

  delete() {
    debugger
    if (confirm('Deseja realmente desativar esse Insumo?')) {
      this.authService.user$.subscribe(token => {
        this.service.deleteInsumo(this.form.value, token)
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
  }

  cancelar() {
    if (confirm('Cancelar as mudanças?')) {
      this.vm.Registros = this.vm.LimpaRegistros();
      this.router.navigateByUrl("/");
    }
  }
}
