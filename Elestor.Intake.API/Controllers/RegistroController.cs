using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data;



namespace Elestor.Intake.API.Controllers
{
    [Produces("application/json")]
    [Route("api/registro")]
    public class RegistroController : Controller
    {
        readonly IRegistro _registro;
        readonly ILogin _login;
        
        public RegistroController(IRegistro registro, ILogin login)
        {
            _registro = registro ?? throw new ArgumentNullException(nameof(registro), "Cannot be null.");
            _login = login ?? throw new ArgumentNullException(nameof(login), "Cannot be null.");
        }

        [HttpPost("usuario")]
        public  async Task<object> Registro([FromBody] Usuario userModel)
        {

            object response = null;
            IEnumerable<Usuario> usuario = null;
            try
            {
                response = await _registro.Registro(userModel);

                if(Convert.ToBoolean(response))
                {
                    usuario = await _login.Login(userModel);
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = e.Message
                };
            }
            return  usuario;
        }
    }
}
