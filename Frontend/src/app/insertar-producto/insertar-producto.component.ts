import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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

  constructor(private http: HttpClient) {}

  // Lógica para guardar el producto en la base de datos
  guardarProducto() {
    const Producto = {
      IdProducto: this.idProducto,
      NombreProducto: this.nombreProducto,
      Costo: this.costo, 
      Precio: this.precio
    };

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

