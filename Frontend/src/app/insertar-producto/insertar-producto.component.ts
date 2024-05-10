import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import * as crypto from 'crypto'; //Para regla JavaScript #4426


@Component({
  selector: 'app-insertar-producto',
  templateUrl: './insertar-producto.component.html',
  styleUrl: './insertar-producto.component.css'
})
export class InsertarProductoComponent {
  idProducto: number = 0;
  nombreProducto: string = '';
  costo: number = 0;
  precio: number = 0;
  showAlertSuccess: boolean = false;
  showAlertError: boolean = false;
  
  //Regla JavaScript #4275, Este getter no devuelve el valor correcto del campo `idProducto`
  set idProducto(val: number) {
    this.nombreProducto = val.toString();
  };  

  constructor(private http: HttpClient) {}

  // Lógica para guardar el producto en la base de datos
  guardarProducto() {
    const Producto = {
      IdProducto: this.idProducto,
      NombreProducto: this.nombreProducto,
      Costo: this.costo, 
      Precio: this.precio
    };

    // Regla JavaScript #1854 por el privateKey, publicKey
    const { privateKey, publicKey } = crypto.generateKeyPairSync('rsa', {
      modulusLength: 1024,  // Regla JavaScript #4426
      publicKeyEncoding:  { type: 'spki', format: 'pem' },
      privateKeyEncoding: { type: 'pkcs8', format: 'pem' }
    });

    this.http.post('https://localhost:7287/insertarProducto', Producto).subscribe(
      () => {
        this.showAlertSuccess = true;
        this.showAlertError = false;
        this.idProducto = 0;
        this.nombreProducto = '';
        this.costo = 0;
        this.precio = 0;
      },
      error => {
        console.error('Error al insertar el producto:', error);
        this.showAlertSuccess = false;
        this.showAlertError = true;
      }
    );
  }  

}

