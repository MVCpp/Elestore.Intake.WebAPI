using System;
namespace Elestor.Intake.API.Models
{
    public class Producto
    {
        public Producto()
        {
        }

        public string clave { get; set; } = String.Empty;
        public string complemento { get; set; } = String.Empty;
        public string descripcion { get; set; } = String.Empty;
        public int estatus { get; set; } = -1;
        //public byte[] fotografia { get; set; } = new byte[] { };
        public string fotografia { get; set; } = string.Empty;
        public string id_catProducto { get; set; } = string.Empty;
        public int id_producto { get; set; } = -1;
        public string negocioid { get; set; } = String.Empty;
        public string nombre { get; set; } = String.Empty;
        public string otracategoria { get; set; } = String.Empty;
        public double precio { get; set; } = 0.0;
        public double tiempopreparacion { get; set; } = 0.0;
        public int FK_idNegocio { get; set; } = 0;
    }
}
