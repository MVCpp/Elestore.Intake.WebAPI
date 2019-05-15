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
        //public byte[] fotografia { get; set; } = new byte[] { };
        public string fotografia { get; set; } = string.Empty;
        public double cantidad { get; set; } = 0.0;
        public double precio { get; set; } = 0.0;
        public string negocioid { get; set; } = String.Empty;

    }
}
