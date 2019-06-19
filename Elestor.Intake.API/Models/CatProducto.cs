using System;
namespace Elestor.Intake.API.Models
{
    public class CatProducto
    {
        public CatProducto()
        {

        }

        public int id_catProducto { get; set; } = 0;
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int clave { get; set; } = 0;
        public int estatus { get; set; } = 0;
    }
}
