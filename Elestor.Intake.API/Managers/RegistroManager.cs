using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class RegistroManager : IRegistro
    {
        readonly IDataAccess _dataAccess;

        public RegistroManager(IDataAccess dataAcces)
        {
            _dataAccess = dataAcces;
        }

        public async Task<bool> Registro(Usuario usuario)
        {
            bool response = false;

            try
            {

                response = await _dataAccess.Registro(usuario);
            }
            catch(Exception ex)
            {
                response = false;
            }

            return response;
        }
    }
}
