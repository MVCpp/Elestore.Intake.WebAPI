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
    [Route("api/negocio")]
    public class NegocioController
    {
        readonly INegocio _negocio;

        public NegocioController(INegocio negocio)
        {
            _negocio = negocio;
        }

        [HttpPost("agregar")]
        public async Task<object> AgregarNegocio([FromBody] Negocio negocio)
        {
            object response = null;

            try
            {
                response = await _negocio.AgregarNegocio(negocio);
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

        [HttpPost("obtener")]
        public async Task<object> ObtenerNegocio([FromBody] string clientid)
        {
            object response = null;

            try
            {
                response = await _negocio.ObtenerNegocio(clientid);
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



        [HttpPost("catnegocio")]
        public async Task<object> ObtenerCatNegocio([FromBody] int id = 0)
        {
            object response = null;

            try
            {
                response = await _negocio.ObtenerCatNegocio();
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

        [HttpPost("subcatnegocio")]
        public async Task<object> ObtenerSubCatNegocio([FromBody]int id)
        {
            object response = null;

            try
            {
                response = await _negocio.ObtenerSubCatNegocio(id);
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
