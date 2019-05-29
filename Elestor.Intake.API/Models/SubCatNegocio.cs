using System;
namespace Elestor.Intake.API.Models
{
    public class SubCatNegocio
    {
        public SubCatNegocio()
        {
        }

        public string nombre { get; set; } = String.Empty;

        public string descripcion { get; set; }= String.Empty;

        public int FK_CATNEGOCIO { get; set; } = -1;

        public int id_catNegocio { get; set; } = -1;
    }
}
