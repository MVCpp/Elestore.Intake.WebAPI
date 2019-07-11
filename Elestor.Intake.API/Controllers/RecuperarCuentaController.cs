using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Log;
using Elestor.Intake.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elestor.Intake.API.Controllers
{
    [Produces("application/json")]
    [Route("api/cuenta")]
    public class RecuperarCuentaController : Controller
    {
        readonly IRecuperarCuenta _recuperarCuenta;
        readonly ILog _log;

        public RecuperarCuentaController(IRecuperarCuenta recuperarCuenta, ILog log)
        {
            _recuperarCuenta = recuperarCuenta ?? throw new ArgumentNullException(nameof(recuperarCuenta), "Cannot be null.");
            _log = log ?? throw new ArgumentNullException(nameof(log), "Cannot be null.");
        }

        [HttpPost("recuperar")]
        public async Task<object> Recuperar([FromBody] Usuario userModel)
        {
            if (userModel == null)
            {
                _log.Error(nameof(userModel).ToString() + "Cannot be null.");
                throw new ArgumentNullException(nameof(userModel), "Cannot be null.");
            }

            object response = null;
            try
            {
                response = await _recuperarCuenta.RecuperarCuenta(userModel);
                _log.Information("Response from RecuperarCuenta");
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


        [HttpPost("actualizar")]
        public async Task<object> Actualizar([FromBody] Usuario userModel)
        {
            object response = null;

            if (userModel == null)
            {
                _log.Error(nameof(userModel).ToString() + "Cannot be null.");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(nameof(userModel) + "Cannot be null."),
                    ReasonPhrase = "Actualizar"
                };
            }

            try
            {
                response = await _recuperarCuenta.Actualizar(userModel);
                _log.Information("Response from RecuperarCuenta");
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

    }
}
