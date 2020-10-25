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
}
