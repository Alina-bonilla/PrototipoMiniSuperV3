using System.ComponentModel.DataAnnotations;

namespace MiniSuper.Models
{ 
    public class Producto
    {
        [Key] public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
    }
}

