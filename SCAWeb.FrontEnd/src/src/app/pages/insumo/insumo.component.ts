import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr/toastr/toastr.service';
import { AuthService } from 'src/app/core';
import { DataService } from 'src/app/data.service';

@Component({
  selector: 'app-insumo',
  templateUrl: './insumo.component.html',
  styleUrls: ['./insumo.component.css']
})
export class InsumoComponent implements OnInit {
  public form: FormGroup;
  public insumo = {
    Id: '',
    descricao_insumo: '',
    status_insumo: null,
    data_aquisicao: '',
    data_atualizacao: '',
    qtd_dias_manut_prev: null,
    id_tipo_insumo: '',
    id_fornec_insumo: '',
    user: ''
  }
  public listTipoInsumo = [];
  public listFornecedor = [];

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
      descricao_insumo: '',
      status_insumo: null,
      data_aquisicao: '',
      data_atualizacao: '',
      qtd_dias_manut_prev: null,
      id_tipo_insumo: '',
      id_fornec_insumo: '',
      user: ''
    });
  }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const token = x;
      this.service.getAllTipoInsumo(accessToken)
        .subscribe(
          (data: any) => {
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

  submit() {
    this.authService.user$.subscribe(x => {
      const accessToken = localStorage.getItem('access_token');
      const token = x;
      this.insumo.descricao_insumo = this.form.value.descricao_insumo;
      this.insumo.data_aquisicao = new Date(this.form.value.data_aquisicao).toJSON();
      this.insumo.qtd_dias_manut_prev = this.form.value.qtd_dias_manut_prev;
      this.insumo.id_tipo_insumo = this.form.value.id_tipo_insumo;
      this.insumo.id_fornec_insumo = this.form.value.id_fornec_insumo;

      this.service.postInsumo(this.insumo, accessToken)
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

  update() {
    this.authService.user$.subscribe(token => {
      this.insumo.Id = this.form.value.Id;
      this.insumo.descricao_insumo = this.form.value.descricao_insumo;
      this.insumo.status_insumo = this.form.value.status_insumo;
      this.insumo.qtd_dias_manut_prev = this.form.value.qtd_dias_manut_prev;

      this.service.putInsumo(this.insumo, token)
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

  delete(data: any) {
    if (confirm('Deseja realmente excluir esse Insumo?')) {
      this.authService.user$.subscribe(token => {
        this.service.deleteInsumo(data, token)
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
      this.insumo = {
        Id: '',
        descricao_insumo: '',
        status_insumo: null,
        data_aquisicao: '',
        data_atualizacao: '',
        qtd_dias_manut_prev: null,
        id_tipo_insumo: '',
        id_fornec_insumo: '',
        user: ''
      }
      this.router.navigateByUrl("/");
    }
  }
}
