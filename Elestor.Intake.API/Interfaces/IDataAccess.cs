using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface IDataAccess
    {
        Task<object> Registro(Usuario usuario);

        Task<object> Login(Usuario usuario);

        Task<object> RecuperarCuenta(Usuario usuario);
    }
}
