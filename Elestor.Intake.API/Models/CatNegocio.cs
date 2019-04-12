using System;
namespace Elestor.Intake.API.Models
{
    public class CatNegocio
    {
        public CatNegocio()
        {
        }

        public int id_catNegocio { get; set; } = -1;

        public string nombre { get; set; } = String.Empty;

        public string descripcion { get; set; } = String.Empty;
    }
}
