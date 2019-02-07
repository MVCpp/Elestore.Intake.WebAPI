using System;
using Elestor.Intake.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elestor.Intake.API.Controllers
{
    [Produces("application/json")]
    [Route("api/registro")]
    public class RegistroController : Controller
    {
        public RegistroController()
        {
        }

        [HttpGet]
        public IActionResult cadena(string cad)
        {
            EmptyClass emptyClass = new EmptyClass();

            emptyClass.cadena = "Hola";
            return Ok(emptyClass);
        }
    }
}
