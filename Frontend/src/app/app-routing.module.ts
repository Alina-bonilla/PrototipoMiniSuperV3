import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InsertarProductoComponent } from './insertar-producto/insertar-producto.component';
import { RankingSalarioComponent } from './ranking-salario/ranking-salario.component';

const routes: Routes = [
  { path: 'insertar-producto', component: InsertarProductoComponent }, 
  { path: 'ranking-salario', component: RankingSalarioComponent }, 
  { path: '', redirectTo: '/insertar-producto', pathMatch: 'full' },



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
