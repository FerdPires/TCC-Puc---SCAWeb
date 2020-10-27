import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { TesteComponent } from './pages/teste/teste.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
//import { CoreModule } from './core/core.module';
import { InsumoComponent } from './pages/insumo/insumo.component';
import { ListInsumosComponent } from './pages/list-insumos/list-insumos.component';
import { AgGridModule } from 'ag-grid-angular';
import { ManutencaoInsumoComponent } from './pages/manutencao-insumo/manutencao-insumo.component';
import { ButtonDeleteRendererComponent } from './renderer/button-delete-renderer.component';
import { ButtonDetailsRendererComponent } from './renderer/button-details-renderer.component';
import { ButtonEditRendererComponent } from './renderer/button-edit-renderer.component';
import { ToastrModule } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { NewInsumoComponent } from './pages/new-insumo/new-insumo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgendaManutencaoComponent } from './pages/agenda-manutencao/agenda-manutencao.component';
import { ListAgendamentosComponent } from './pages/list-agendamentos/list-agendamentos.component';
import { ListManutencaoComponent } from './pages/list-manutencao/list-manutencao.component';
import { AreasRiscoComponent } from './pages/areas-risco/areas-risco.component';
import { SensorAreaRiscoComponent } from './pages/sensor-area-risco/sensor-area-risco.component';
import { AlertasComponent } from './pages/alertas/alertas.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    TesteComponent,
    InsumoComponent,
    ListInsumosComponent,
    ManutencaoInsumoComponent,
    ButtonDeleteRendererComponent,
    ButtonDetailsRendererComponent,
    ButtonEditRendererComponent,
    NewInsumoComponent,
    AgendaManutencaoComponent,
    ListAgendamentosComponent,
    ListManutencaoComponent,
    AreasRiscoComponent,
    SensorAreaRiscoComponent,
    AlertasComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    //CoreModule,
    ToastrModule.forRoot(),
    CommonModule,
    BrowserAnimationsModule,
    AgGridModule.withComponents([ButtonDeleteRendererComponent, ButtonDetailsRendererComponent, ButtonEditRendererComponent])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
