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
    [Route("api/cuenta")]
    public class RecuperarCuentaController : Controller
    {
        readonly IRecuperarCuenta _recuperarCuenta;

        public RecuperarCuentaController(IRecuperarCuenta recuperarCuenta)
        {
            _recuperarCuenta = recuperarCuenta ?? throw new ArgumentNullException(nameof(recuperarCuenta), "Cannot be null."); ;
        }

        [HttpPost("recuperar")]
        public async Task<object> Recuperar([FromBody] Usuario userModel)
        {
            object response = null;
            try
            {
                response = await _recuperarCuenta.RecuperarCuenta(userModel);
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


        [HttpPost("actualizar")]
        public async Task<object> Actualizar([FromBody] Usuario userModel)
        {
            object response = null;
            try
            {
                response = await _recuperarCuenta.Actualizar(userModel);
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

    }
}
