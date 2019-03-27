using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elestor.Intake.API.Controllers
{
    [Produces("application/json")]
    [Route("api/usuario")]
    public class LoginController : Controller
    {
        readonly ILogin _login;
        private Usuario usuario;

        public LoginController(ILogin login)
        {
            _login = login ?? throw new ArgumentNullException(nameof(login), "Cannot be null.");
            usuario = new Usuario();
        }

        [HttpPost("inicio")]
        public async Task<object> IniciarSesion([FromBody] Usuario userModel)
        {
            object response = null;

            try
            {
                response = await _login.Login(userModel);
            }
            catch (Exception e)
            {

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
