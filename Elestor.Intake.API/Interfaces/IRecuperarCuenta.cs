using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface IRecuperarCuenta
    {
        Task<object> RecuperarCuenta(Usuario usuario);
    }
}
