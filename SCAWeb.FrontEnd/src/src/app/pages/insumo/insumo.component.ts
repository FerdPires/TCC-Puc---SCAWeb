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
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
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
        this.estadoTela = Operacao.C;
        this.form = this.fb.group({
          Id: history.state.data.id,
          descricao_insumo: history.state.data.descricao_insumo,
          data_aquisicao: new Date(history.state.data.data_aquisicao).toJSON().substring(0, 10),
          qtd_dias_manut_prev: history.state.data.qtd_dias_manut_prev,
          id_tipo_insumo: history.state.data.id_tipo_insumo,
          id_fornec_insumo: history.state.data.id_fornec_insumo
        });
        this.status_insumo = history.state.data.status_insumo == 1 ? "ATIVO" : (history.state.data.status_insumo == 2 ? "INATIVO" : "EM MANUTENÇÃO");
        // this.form.value.descricao_insumo = history.state.data.descricao_insumo;
        // this.form.value.data_aquisicao = new Date(history.state.data.data_aquisicao).toJSON().substring(0, 10);
        // this.form.value.qtd_dias_manut_prev = history.state.data.qtd_dias_manut_prev;
        // this.form.value.id_tipo_insumo = history.state.data.id_tipo_insumo;
        // this.form.value.id_fornec_insumo = history.state.data.id_fornec_insumo;
        // this.form.value.status_insumo = history.state.data.status_insumo == 1 ? "ATIVO" : (history.state.data.status_insumo == 2 ? "INATIVO" : "EM MANUTENÇÃO");
      }
      // else {
      //   this.estadoTela = Operacao.I;
      //   this.status_insumo = "ATIVO";
      //   // if (this.estadoTela == Operacao.E || this.estadoTela == Operacao.C) {
      //   //   this.service.getByIdInsumo(this.id_insumo, accessToken)
      //   //     .subscribe(
      //   //       (data: any) => {
      //   //         debugger
      //   //         //this.listTipoInsumo = data;
      //   //       }
      //   //     );
      //   // }
      // }

    });
  }

  // salvar() {
  //   if (this.estadoTela == Operacao.I) {
  //     this.authService.user$.subscribe(x => {
  //       debugger
  //       const accessToken = localStorage.getItem('access_token');
  //       const token = x;
  //       this.vm.Registros.descricao_insumo = this.form.value.descricao_insumo;
  //       this.vm.Registros.data_aquisicao = new Date(this.form.value.data_aquisicao).toJSON();
  //       this.vm.Registros.qtd_dias_manut_prev = this.form.value.qtd_dias_manut_prev;
  //       this.vm.Registros.id_tipo_insumo = this.form.value.id_tipo_insumo;
  //       this.vm.Registros.id_fornec_insumo = this.form.value.id_fornec_insumo;

  //       this.service.postInsumo(this.vm.Registros, accessToken)
  //         .subscribe((res: any) => {
  //           if (res.success) {
  //             this.toastr.success(res.message);
  //           } else {
  //             this.toastr.error(res.message);
  //           }
  //           this.router.navigateByUrl("/");
  //         });
  //     });
  //   }
  //   else if (this.estadoTela == Operacao.E) {
  //     this.update();
  //   }
  // }

  // update() {
  //   this.authService.user$.subscribe(token => {
  //     debugger
  //     this.vm.Registros.Id = this.form.value.Id;
  //     this.vm.Registros.descricao_insumo = this.form.value.descricao_insumo;
  //     this.vm.Registros.status_insumo = this.status_insumo == "ATIVO" ? StatusInsumo.Ativo :
  //       (this.status_insumo == "INATIVO" ? StatusInsumo.Inativo : StatusInsumo.Manutencao);
  //     this.vm.Registros.qtd_dias_manut_prev = this.form.value.qtd_dias_manut_prev;

  //     this.service.putInsumo(this.vm.Registros, token)
  //       .subscribe((res: any) => {
  //         if (res.success) {
  //           this.toastr.success(res.message);
  //         } else {
  //           this.toastr.error(res.message);
  //         }
  //         this.router.navigateByUrl("/");
  //       });
  //   });
  // }

  // delete() {
  //   debugger
  //   if (confirm('Deseja realmente desativar esse Insumo?')) {
  //     this.authService.user$.subscribe(token => {
  //       this.service.deleteInsumo(this.form.value, token)
  //         .subscribe((res: any) => {
  //           if (res.success) {
  //             this.toastr.success(res.message);
  //           } else {
  //             this.toastr.error(res.message);
  //           }
  //           this.router.navigateByUrl("/");
  //         });
  //     });
  //   }
  // }

  // cancelar() {
  //   if (confirm('Cancelar as mudanças?')) {
  //     this.vm.Registros = this.vm.LimpaRegistros();
  //     this.router.navigateByUrl("/");
  //   }
  // }
}
