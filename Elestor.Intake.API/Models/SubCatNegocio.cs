using System;
namespace Elestor.Intake.API.Models
{
    public class SubCatNegocio
    {
        public SubCatNegocio()
        {
        }

        string nombre { get; set; } = String.Empty;

        string descripcion { get; set; }= String.Empty;

        int FK_CATNEGOCIO { get; set; } = -1;
    }
}
