import { Component,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-ranking-salario',
  templateUrl: './ranking-salario.component.html',
  styleUrl: './ranking-salario.component.css'
})
export class RankingSalarioComponent implements OnInit {
  top3Empleados: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.obtenerTop3SalarioBruto();
  }

  obtenerTop3SalarioBruto() {
    this.http.get<any[]>('https://localhost:7287/top3salariobruto').subscribe(
      (data) => {
        this.top3Empleados = data;
        //Para la regla
        //this.encriptarCodigosIngreso(data,secretKey); // Llama a la función de encriptación después de obtener los datos
      },
      (error) => {
        console.error('Error al obtener el top 3 de empleados:', error);
      }
    );
  }

  openDangerousLink() {
    // Abrir una ventana emergente sin el atributo "noopener"
    const popup = window.open("http://localhost:4200/dangerous");
  
    // Verificar si la ventana emergente se abrió correctamente
    if (popup && !popup.closed) {
      // Modificar el contenido de la ventana de origen desde la ventana emergente
      popup.onload = () => {
        popup.opener.document.body.innerHTML = "<h1>¡Has sido hackeado!</h1>";
      };
    } else {
      // Manejar el caso en el que la ventana emergente no se pudo abrir
      console.error("No se pudo abrir la ventana emergente");
    }
  }
}