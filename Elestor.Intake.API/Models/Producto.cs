using System;
namespace Elestor.Intake.API.Models
{
    public class Producto
    {
        public Producto()
        {
        }

        public int id_producto { get; set; } = -1;
        public string nombre { get; set; } = String.Empty;
        public string descripcion { get; set; } =  String.Empty;
        public string clave { get; set; } = String.Empty;
        public int estatus { get; set; } = -1;
        public string fotografia { get; set; } = String.Empty;
        public double cantidad { get; set; } = 0.0;
        public double precio { get; set; } = 0.0;
        public int FK_idNegocio { get; set; } = -1;
        public string negocioid { get; set; } = String.Empty;

    }
}
