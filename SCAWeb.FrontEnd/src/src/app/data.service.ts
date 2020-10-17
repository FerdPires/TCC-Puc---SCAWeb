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

  public teste() {
    return this.http.get(`${environment.apiUrl}/authenticated`);
    //return this.http.get(`${this.baseUrl}/v1/leiloes/user`, { headers: this.composeHeaders(token) });
  }
}
