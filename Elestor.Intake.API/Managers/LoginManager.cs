using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class LoginManager : ILogin
    {
        readonly IDataAccess _dataAccess;
        
        public LoginManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<object> Login(Usuario usuario)
        {
            return await _dataAccess.Login(usuario);
        }
    }
}
