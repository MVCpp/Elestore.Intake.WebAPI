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
    [Route("api/negocio/producto")]
    public class ProductoController
    {
        readonly IProducto _producto;

        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }

        [HttpPost("obtener")]
        public async Task<object> ObtenerProducto([FromBody]Negocio negocio)
        {
            object response = null;

            try
            {
                response = await _producto.ObtenerProducto(negocio);
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


        [HttpPost("agregar")]
        public async Task<object> GuardarProductos([FromBody]Producto producto)
        {
            object response = null;

            try
            {
                response = await _producto.GuardarProducto(producto);
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
