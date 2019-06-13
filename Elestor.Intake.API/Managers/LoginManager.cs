using System;
using System.Collections;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Usuario>> Login(Usuario usuario)
        {
            IEnumerable<Usuario> response;

            try
            {
                response = await _dataAccess.Login(usuario);
            }
            catch(Exception ex)
            {
                return null;
            }

            return response;
        }


    }
}
