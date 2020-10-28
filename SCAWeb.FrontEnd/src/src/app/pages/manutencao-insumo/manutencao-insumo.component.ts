import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  public data_inicio_manut = "";
  public status_manut = "";
  public listInsumos = [];

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
      data_inicio: moment(new Date()).toJSON().substring(0, 10),
      data_fim: '',
      id_insumo: ''
    });
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      if (history.state.data) {
        this.estadoTela = history.state.estadoTela;
        this.buscaItensManutencao(history.state.data.tipo_manutencao);
        // if (history.state.data.status_manutencao == 1) {
        //   this.service.GetAllPreventiva(accessToken)
        //     .subscribe(
        //       (data: any) => {
        //         data.forEach(element => {
        //           for (let y = 0; y < this.listInsumos.length; y++) {
        //             const j = this.listInsumos[y];
        //             if (element.id_insumo == j.id) {
        //               this.listInsumosAgendadosAteHoje.push({
        //                 id_insumo: element.id_insumo,
        //                 descricao_insumo: j.descricao_insumo
        //               });
        //               break;
        //             }
        //           }
        //         });
        //       }
        //     );
        // } else {
        //   this.service.GetAllCorretiva(accessToken)
        //     .subscribe(
        //       (data: any) => {
        //         data.forEach(element => {
        //           for (let y = 0; y < this.listInsumos.length; y++) {
        //             const j = this.listInsumos[y];
        //             if (element.id_insumo == j.id) {
        //               this.listInsumosAgendadosAteHoje.push({
        //                 id_insumo: element.id_insumo,
        //                 descricao_insumo: j.descricao_insumo
        //               });
        //               break;
        //             }
        //           }
        //         });
        //       }
        //     );
        // }

        this.status_manut = history.state.data.status_manutencao == 1 ? "INICIADA" : "CONCLUÍDA";
        this.form = this.fb.group({
          Id: history.state.data.id,
          tipo_manutencao: history.state.data.tipo_manutencao,
          descricao_manutencao: history.state.data.descricao_manutencao,
          data_inicio: new Date(history.state.data.data_inicio).toJSON().substring(0, 10),
          data_fim: new Date(history.state.data.data_fim).toJSON().substring(0, 10),
          id_insumo: history.state.data.id_insumo
        });
      }
      else {
        this.estadoTela = Operacao.I;
        //  this.data_inicio_manut = moment(new Date()).format("DD/MM/YYYY");
        this.status_manut = "INICIADA";
      }
    });
  }

  buscaItensManutencao(e: any) {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      if (this.estadoTela != Operacao.I) {
        var val = e;
      } else {
        var val = e.target.value;
      }
      this.listInsumosAgendadosAteHoje = [];
      this.service.getAllInsumos(accessToken)
        .subscribe(
          (insumos: any) => {
            this.listInsumos = insumos;

            if (val == "1") {
              this.service.GetAllPreventiva(accessToken)
                .subscribe(
                  (data: any) => {
                    data.forEach(element => {
                      for (let y = 0; y < this.listInsumos.length; y++) {
                        const j = this.listInsumos[y];
                        if (element.id_insumo == j.id) {
                          this.listInsumosAgendadosAteHoje.push({
                            id_insumo: element.id_insumo,
                            descricao_insumo: j.descricao_insumo
                          });
                          break;
                        }
                      }
                    });
                  }
                );
            } else if (val == "2") {
              this.service.GetAllCorretiva(accessToken)
                .subscribe(
                  (data: any) => {
                    data.forEach(element => {
                      for (let y = 0; y < this.listInsumos.length; y++) {
                        const j = this.listInsumos[y];
                        if (element.id_insumo == j.id) {
                          this.listInsumosAgendadosAteHoje.push({
                            id_insumo: element.id_insumo,
                            descricao_insumo: j.descricao_insumo
                          });
                          break;
                        }
                      }
                    });
                  }
                );
            }
          }
        );
    });
  }

  salvar() {
    this.vm.Registros = {};
    if (this.estadoTela == Operacao.I) {
      this.authService.user$.subscribe(x => {
        const accessToken = localStorage.getItem('access_token');
        this.vm.Registros.tipo_manutencao = Number(this.form.value.tipo_manutencao);
        this.vm.Registros.descricao_manutencao = this.form.value.descricao_manutencao;
        //this.vm.Registros.status_manutencao = this.form.value.qtd_dias_manut_prev;
        this.vm.Registros.data_inicio = new Date(this.form.value.data_inicio).toJSON();
        //this.vm.Registros.data_fim = this.form.value.id_fornec_insumo;
        this.vm.Registros.id_insumo = this.form.value.id_insumo;

        this.service.postManutencao(this.vm.Registros, accessToken)
          .subscribe((res: any) => {
            if (res.success) {
              this.toastr.success(res.message);
            } else {
              this.toastr.error(res.message);
            }
            //     this.router.navigateByUrl("/");
          });
      });
    }
    else if (this.estadoTela == Operacao.E) {
      this.update();
    }

  }

  update() {
    this.authService.user$.subscribe(x => {
      debugger
      const accessToken = localStorage.getItem('access_token');
      this.vm.Registros.Id = this.form.value.Id;
      this.vm.Registros.descricao_manutencao = this.form.value.descricao_manutencao;
      //this.vm.Registros.tipo_manutencao = Number(this.form.value.tipo_manutencao);
      this.vm.Registros.status_manutencao = this.status_manut == "INICIADA" ? 1 : 2;
      //this.vm.Registros.id_insumo = this.form.value.id_insumo;
      this.vm.Registros.data_inicio = new Date(this.form.value.data_inicio).toJSON();
      this.vm.Registros.data_fim = new Date(this.form.value.data_fim).toJSON();

      if (this.form.value.tipo_manutencao == 1) {
        this.service.putManutCorretiva(this.vm.Registros, accessToken)
          .subscribe((res: any) => {
            if (res.success) {
              this.toastr.success(res.message);
            } else {
              this.toastr.error(res.message);
            }
            //  this.router.navigateByUrl("/");
          });
      } else if (this.form.value.tipo_manutencao == 2) {
        this.service.putManutPreventiva(this.vm.Registros, accessToken)
          .subscribe((res: any) => {
            if (res.success) {
              this.toastr.success(res.message);
            } else {
              this.toastr.error(res.message);
            }
            // this.router.navigateByUrl("/");
          });
      }
    });
  }

  finalizarManutencao() {
    this.vm.Registros.status_manutencao = 2;
    this.update();
  }

  cancelar() {
    if (confirm('Cancelar as mudanças?')) {
      this.vm.Registros = this.vm.LimpaRegistros();
      this.router.navigateByUrl("/");
    }
  }

}
