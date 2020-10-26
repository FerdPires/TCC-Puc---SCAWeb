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
    ButtonEditRendererComponent
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
    AgGridModule.withComponents([ButtonDeleteRendererComponent, ButtonDetailsRendererComponent, ButtonEditRendererComponent])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
