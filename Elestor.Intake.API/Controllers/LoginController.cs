using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elestor.Intake.API.Helpers;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Log;
using Elestor.Intake.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elestor.Intake.API.Controllers
{
    [Produces("application/json")]
    [Route("api/usuario")]
    public class LoginController : Controller
    {
        readonly ILogin _login;
        readonly ILog _log;
        private Usuario usuario;

        public LoginController(ILogin login, ILog log)
        {
            _login = login ?? throw new ArgumentNullException(nameof(login), "Cannot be null.");
            _log = log ?? throw new ArgumentNullException(nameof(log), "Cannot be null.");

            usuario = new Usuario();
        }

        [HttpPost("inicio")]
        public async Task<object> IniciarSesion([FromBody] Usuario userModel)
        {
            object response = null;

            if (userModel == null)
            {
                _log.Error(nameof(userModel).ToString() +  "Cannot be null.");
                throw new ArgumentNullException(nameof(userModel), "Cannot be null.");
            }

            try
            {
                response = await _login.Login(userModel);
                _log.Information("Response from IniciarSesion");
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());

                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = e.Message
                };
            }
            return response;
        }



        [HttpPost("hola")]
        public string hola()
        {
            return "hi from aws";
        }

    }
}
