using System.ComponentModel.DataAnnotations;

namespace MiniSuper.Models
{
    public class Empleado
    {
        [Key] public int Cedula { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public string? Puesto { get; set; }
        public string? CodigoIngreso { get; set; }
        public decimal SalarioMensualBruto { get; set; }
        public decimal SalarioMensualNeto { get; set; }
    }
}
