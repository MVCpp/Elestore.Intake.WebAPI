using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class RegistroManager : IRegistro
    {
        public RegistroManager()
        {
        }

        public async Task<object> Registro(Usuario usuario)
        {
            object response = null;
            await Task.Run(() => {
                // CAll db
            });
            return response;
        }
    }
}
