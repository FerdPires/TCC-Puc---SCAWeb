import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { TesteComponent } from './pages/teste/teste.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { InsumoComponent } from './pages/insumo/insumo.component';
import { ListInsumosComponent } from './pages/list-insumos/list-insumos.component';
import { AgGridModule } from 'ag-grid-angular/lib/ag-grid-angular.module';
import { ManutencaoInsumoComponent } from './manutencao-insumo/manutencao-insumo.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    TesteComponent,
    InsumoComponent,
    ListInsumosComponent,
    ManutencaoInsumoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CoreModule,
    AgGridModule.withComponents([])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
