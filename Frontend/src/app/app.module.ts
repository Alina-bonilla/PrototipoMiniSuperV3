import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InsertarProductoComponent } from './insertar-producto/insertar-producto.component';

import { FormsModule } from '@angular/forms';
import { MenuComponent } from './menu/menu.component';
import { RankingSalarioComponent } from './ranking-salario/ranking-salario.component';

import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    InsertarProductoComponent,
    MenuComponent,
    RankingSalarioComponent
  ],
  
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
