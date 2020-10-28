import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';
import { AgendaManutencaoModel } from 'src/app/models/agenda-manutencao-model';
import { Operacao } from 'src/app/models/Enums/operacao.enum';

@Component({
  selector: 'app-agenda-manutencao',
  templateUrl: './agenda-manutencao.component.html',
  styleUrls: ['./agenda-manutencao.component.css']
})
export class AgendaManutencaoComponent implements OnInit {
  public estadoTela: Operacao;
  public form: FormGroup;
  public vm: AgendaManutencaoModel = new AgendaManutencaoModel();

  public listInsumosAtivos = [];
  public status_agenda = "";
  public tipo_manutencao = "";

  constructor(
    private fb: FormBuilder,
    private service: DataService,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.form = this.fb.group({
      tipo_manutencao: 1,
      status_agenda: null,
      data_manutencao: '',
      data_atualizacao: '',
      id_insumo: ''
    });
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');

      this.tipo_manutencao = "CORRETIVA";
      this.status_agenda = "ABERTO";
      this.service.getAllInsumosAtivos(accessToken)
        .subscribe(
          (data: any) => {
            data.forEach(element => {
              this.listInsumosAtivos.push({ id_insumo: element.id, descricao_insumo: element.descricao_insumo });
            });
          }
        );

      if (history.state.data) {
        this.estadoTela = Operacao.E;
        this.form = this.fb.group({
          Id: history.state.data.Id,
          data_manutencao: new Date(history.state.data.data_manutencao).toJSON().substring(0, 10),
          id_insumo: history.state.data.id_insumo
        });
        this.status_agenda = history.state.data.status_agenda == 1 ? "ABERTO" : "FECHADO";
      }
      else {
        this.estadoTela = Operacao.I;
      }
    });
  }

  salvar() {
    this.vm.Registros = {};
    if (this.estadoTela == Operacao.I) {
      this.authService.user$.subscribe(x => {
        const accessToken = localStorage.getItem('access_token');
        this.vm.Registros.data_manutencao = new Date(this.form.value.data_manutencao).toJSON();
        this.vm.Registros.id_insumo = this.form.value.id_insumo;

        this.service.postAgendaManut(this.vm.Registros, accessToken)
          .subscribe((res: any) => {
            if (res.success) {
              this.toastr.success(res.message);
            } else {
              this.toastr.error(res.message);
            }
            //this.router.navigateByUrl("/");
          });
      });
    }
    else if (this.estadoTela == Operacao.E) {
      this.update();
    }
  }

  update() {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      this.vm.Registros.Id = this.form.value.Id;
      this.vm.Registros.status_agenda = 1;
      this.vm.Registros.data_manutencao = new Date(this.form.value.data_manutencao).toJSON();

      this.service.putAgendaManut(this.vm.Registros, accessToken)
        .subscribe((res: any) => {
          if (res.success) {
            this.toastr.success(res.message);
          } else {
            this.toastr.error(res.message);
          }
          //this.router.navigateByUrl("/");
        });
    });
  }

  delete() {
    if (confirm('Deseja realmente desativar esse Insumo?')) {
      this.authService.user$.subscribe(x => {
        const accessToken = localStorage.getItem('access_token');
        this.service.deleteAgendaManut(this.form.value, accessToken)
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
      //this.router.navigateByUrl("/");
    }
  }

}
