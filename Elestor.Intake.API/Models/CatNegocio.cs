using System;
namespace Elestor.Intake.API.Models
{
    public class CatNegocio
    {
        public CatNegocio()
        {
        }

        int id_catNegocio { get; set; } = -1;

        string nombre { get; set; } = String.Empty;

        string descripcion { get; set; } = String.Empty;
    }
}
