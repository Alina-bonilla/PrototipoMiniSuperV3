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
      },
      (error) => {
        console.error('Error al obtener el top 3 de empleados:', error);
      }
    );
  }
}