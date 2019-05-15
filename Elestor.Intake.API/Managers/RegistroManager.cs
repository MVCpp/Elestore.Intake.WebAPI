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

        public async Task<Usuario> Registro(Usuario usuario)
        {
            Usuario response = null;

            try
            {
                response = new Usuario();
                response = await _dataAccess.Registro(usuario);
            }
            catch(Exception ex)
            {
                response = null;
            }

            return response;
        }
    }
}
