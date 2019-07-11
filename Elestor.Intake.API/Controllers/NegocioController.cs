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
    [Route("api/negocio")]
    public class NegocioController
    {
        readonly INegocio _negocio;
        readonly ILog _log;

        public NegocioController(INegocio negocio, ILog log)
        {
            _negocio = negocio ?? throw new ArgumentNullException(nameof(negocio), "Cannot be null.");
            _log = log ?? throw new ArgumentNullException(nameof(log), "Cannot be null.");
        }

        [HttpPost("agregar")]
        public async Task<object> AgregarNegocio([FromBody] Negocio negocio)
        {
            object response = null;

            if (negocio == null)
            {
                _log.Error(nameof(negocio).ToString() + "Cannot be null.");
                throw new ArgumentNullException(nameof(negocio), "Cannot be null.");
            }

            try
            {
                response = await _negocio.AgregarNegocio(negocio);
                _log.Information("Response from AgregarNegocio");
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

        [HttpPost("obtener")]
        public async Task<object> ObtenerNegocio([FromBody] string clientid)
        {
            object response = null;

            if (string.IsNullOrEmpty(clientid))
            {
                _log.Error(nameof(clientid).ToString() + "Cannot be null.");
                throw new ArgumentNullException(nameof(clientid), "Cannot be null.");
            }

            try
            {
                response = await _negocio.ObtenerNegocio(clientid);
                _log.Information("Response from ObtenerNegocio");
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

        [HttpPost("obtenerall")]
        public async Task<object> ObtenerNegocios()
        {
            object response = null;

            try
            {                
                response = await _negocio.ObtenerNegocios();
                _log.Information("Response from ObtenerNegocio");
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


        [HttpPost("catnegocio")]
        public async Task<object> ObtenerCatNegocio([FromBody] int id = 0)
        {
            object response = null;

            try
            {
                response = await _negocio.ObtenerCatNegocio();
                _log.Information("Response from ObtenerCatNegocio");
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

        [HttpPost("subcatnegocio")]
        public async Task<object> ObtenerSubCatNegocio([FromBody]int id)
        {
            object response = null;

            try
            {
                response = await _negocio.ObtenerSubCatNegocio(id);
                _log.Information("Response from ObtenerSubCatNegocio");
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

        [HttpPost("editar")]
        public async Task<object> NegocioEditar([FromBody] Negocio negocio)
        {
            object response = null;

            if (negocio == null)
            {
                _log.Error(nameof(negocio).ToString() + "Cannot be null.");
                throw new ArgumentNullException(nameof(negocio), "Cannot be null.");
            }

            try
            {
                response = await _negocio.NegocioEditar(negocio);
                _log.Information("Response from NegocioEditar");
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
