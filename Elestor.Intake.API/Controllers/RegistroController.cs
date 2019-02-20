using System;
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
        
        public RegistroController(IRegistro registro)
        {
            _registro = registro ?? throw new ArgumentNullException(nameof(registro), "Cannot be null.");
        }

        [HttpPost("usuario")]
        public  async Task<object> Registro([FromBody] Usuario userModel)
        {

            object response = null;
            try
            {
                response = await _registro.Registro(userModel);
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
