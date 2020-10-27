import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(
    private http: HttpClient
  ) { }

  public composeHeaders(token) {
    if (token) {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return headers;
    } else {
      return null;
    }
  }

  public teste(token) {
    return this.http.get(`${environment.apiUrl}manager`, { headers: this.composeHeaders(token) });
    //return this.http.get(`${this.baseUrl}/v1/leiloes/user`, { headers: this.composeHeaders(token) });
  }

  //----------Insumos
  public getAllInsumosAtivos(token) {
    return this.http.get(`${environment.apiUrl}insumo/ativos`, { headers: this.composeHeaders(token) });
  }

  public getAllInsumos(token) {
    return this.http.get(`${environment.apiUrl}insumo/listar`, { headers: this.composeHeaders(token) });
  }

  public getByIdInsumo(id, token) {
    return this.http.get(`${environment.apiUrl}insumo/obter/` + id, { headers: this.composeHeaders(token) });
  }

  public postInsumo(data, token) {
    return this.http.post(`${environment.apiUrl}insumo/criar`, data, { headers: this.composeHeaders(token) });
  }

  public deleteInsumo(data, token) {
    return this.http.put(`${environment.apiUrl}insumo/excluir`, data, { headers: this.composeHeaders(token) });
  }

  public putInsumo(data, token) {
    return this.http.put(`${environment.apiUrl}insumo/editar`, data, { headers: this.composeHeaders(token) });
  }

  public getAllTipoInsumo(token) {
    return this.http.get(`${environment.apiUrl}tipo-insumo/listar`, { headers: this.composeHeaders(token) });
  }

  public getAllFornecedor(token) {
    return this.http.get(`${environment.apiUrl}fornecedor/listar`, { headers: this.composeHeaders(token) });
  }

  //---------Agenda Manutenção
  public postAgendaManut(data, token) {
    return this.http.post(`${environment.apiUrl}manutencao-corretiva/agendamento`, data, { headers: this.composeHeaders(token) });
  }

  public deleteAgendaManut(data, token) {
    return this.http.put(`${environment.apiUrl}manutencao-corretiva/agendamento/excluir`, data, { headers: this.composeHeaders(token) });
  }

  public putAgendaManut(data, token) {
    return this.http.put(`${environment.apiUrl}manutencao-corretiva/agendamento/editar`, data, { headers: this.composeHeaders(token) });
  }

  public getAllManutAgendadas(token) {
    return this.http.get(`${environment.apiUrl}agendamento/listar`, { headers: this.composeHeaders(token) });
  }

  public GetAllPreventiva(token) {
    return this.http.get(`${environment.apiUrl}manutencao-preventiva/agendamento/listar`, { headers: this.composeHeaders(token) });
  }

  public GetAllCorretiva(token) {
    return this.http.get(`${environment.apiUrl}manutencao-corretiva/agendamento/listar`, { headers: this.composeHeaders(token) });
  }

  //---------Manutenção
  public getAllManutencoes(token) {
    return this.http.get(`${environment.apiUrl}manutencao/listar`, { headers: this.composeHeaders(token) });
  }

  public postManutencao(data, token) {
    return this.http.post(`${environment.apiUrl}manutencao/criar`, data, { headers: this.composeHeaders(token) });
  }

  public putManutCorretiva(data, token) {
    return this.http.put(`${environment.apiUrl}manutencao-corretiva/editar`, data, { headers: this.composeHeaders(token) });
  }

  public putManutPreventiva(data, token) {
    return this.http.put(`${environment.apiUrl}manutencao-preventiva/editar`, data, { headers: this.composeHeaders(token) });
  }

  //--------Monitoramento
  public getAllAreasRisco(token) {
    return this.http.get(`${environment.apiUrl}area-risco/listar`, { headers: this.composeHeaders(token) });
  }

  public getAllSensores(id, token) {
    return this.http.get(`${environment.apiUrl}sensor/listar/` + id, { headers: this.composeHeaders(token) });
  }

  public getAllAlertas(id, token) {
    return this.http.get(`${environment.apiUrl}alerta/listar/` + id, { headers: this.composeHeaders(token) });
  }
}
