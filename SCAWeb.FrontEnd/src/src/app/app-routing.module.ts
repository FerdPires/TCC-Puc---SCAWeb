import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './pages/home/home.component';
import { InsumoComponent } from './pages/insumo/insumo.component';
import { ListInsumosComponent } from './pages/list-insumos/list-insumos.component';
import { LoginComponent } from './pages/login/login.component';
import { NewInsumoComponent } from './pages/new-insumo/new-insumo.component';
import { TesteComponent } from './pages/teste/teste.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', component: HomeComponent, canActivate: [AuthGuard], children: [
      { path: 'teste', component: TesteComponent, canActivate: [AuthGuard] },
      { path: 'insumo', component: InsumoComponent, canActivate: [AuthGuard] },
      { path: 'criar-insumo', component: NewInsumoComponent, canActivate: [AuthGuard] },
      { path: 'editar-insumo', component: NewInsumoComponent, canActivate: [AuthGuard] },
      { path: 'lista-insumo', component: ListInsumosComponent, canActivate: [AuthGuard] }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
