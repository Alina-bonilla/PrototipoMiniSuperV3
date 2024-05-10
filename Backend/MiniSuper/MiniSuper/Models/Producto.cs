using System.ComponentModel.DataAnnotations;

namespace MiniSuper.Models
{ 
    public class Producto
    {
        [Key] public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }

        //Regla JavaScript 4275
        private readonly int _idProducto = 0;
        public int getIdProducto()
        {
            return this._idProducto; // Devuelve un valor incorrecto
        }
    }
}

