using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class RecuperarCuentaManager : IRecuperarCuenta
    {
        public RecuperarCuentaManager()
        {
        }

        public async Task<object> RecuperarCuenta(Usuario usuario)
        {
            object response = null;
            await Task.Run(() => {
                // CAll db
            });
            return response;
        }
    }
}
