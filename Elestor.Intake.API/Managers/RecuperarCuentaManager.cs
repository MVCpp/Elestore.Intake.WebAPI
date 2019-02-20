using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class RecuperarCuentaManager : IRecuperarCuenta
    {
        readonly IDataAccess _dataAccess;

        public RecuperarCuentaManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<object> RecuperarCuenta(Usuario usuario)
        {
            return await _dataAccess.RecuperarCuenta(usuario);
        }
    }
}
