using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elestor.Intake.API.Helpers;
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
            UsuarioResponse usuario = null;

            try
            {
                usuario = new UsuarioResponse();

                var r = await _login.Login(userModel);

                response = r;

                usuario = ToResponse((List<Usuario>)response);
               
            }
            catch (Exception e)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = e.Message
                };
            }
            return usuario;
        }



        [HttpPost("hola")]
        public string hola()
        {
            return "hi from aws";
        }

        
        internal UsuarioResponse ToResponse(List<Usuario> usuario)
        {
            return new UsuarioResponse()
            {
                clientid = usuario[0].clientid,
                nombre = usuario[0].nombre,
                apellidoMaterno = usuario[0].apellidoMaterno,
                apellidoPaterno = usuario[0].apellidoPaterno,
                nombreUsuario = usuario[0].nombreUsuario,
                password = usuario[0].password,
                confirmPassword = usuario[0].confirmPassword,
                email = usuario[0].email,
                numeroTelefonico = usuario[0].numeroTelefonico,
                fotografia = usuario[0].fotografia.GetString(),
                negocio = usuario[0].negocio
            };
        }

    }
}
