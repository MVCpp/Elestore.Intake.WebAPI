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

        [HttpPost("borrar")]
        public async Task<object> BorrarProductos([FromBody]Producto producto)
        {
            IEnumerable<Producto> response = null;

            try
            {
                response = await _producto.BorrarProducto(producto);
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


        [HttpPost("obtenercatprod")]
        public async Task<object> ObtnerCatProductoPorIdCatNegocio([FromBody]string categoria)
        {
            IEnumerable<CatProducto> response = null;

            try
            {
                response = await _producto.ObtnerCatProductoPorIdCatNegocio(categoria);
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
