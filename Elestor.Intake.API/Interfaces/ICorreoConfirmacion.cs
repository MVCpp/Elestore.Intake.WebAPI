using System;
namespace Elestor.Intake.API.Interfaces
{
    public interface ICorreoConfirmacion
    {
        bool EnviarCorreo(string correo);
    }
}
