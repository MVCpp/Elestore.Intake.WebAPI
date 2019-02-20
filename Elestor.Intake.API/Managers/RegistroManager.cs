using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class RegistroManager : IRegistro
    {
        readonly IDataAccess _dataAccess;

        public RegistroManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<object> Registro(Usuario usuario)
        {
           return await _dataAccess.Registro(usuario);
        }
    }
}
