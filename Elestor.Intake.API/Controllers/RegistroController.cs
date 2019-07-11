﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Log;
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
        readonly ILogin _login;
        readonly ILog _log;

        static HttpClient client = new HttpClient();

        public RegistroController(IRegistro registro, ILogin login, ILog log)
        {
            _registro = registro ?? throw new ArgumentNullException(nameof(registro), "Cannot be null.");
            _login = login ?? throw new ArgumentNullException(nameof(login), "Cannot be null.");
            _log = log ?? throw new ArgumentNullException(nameof(log), "Cannot be null.");
        }

        [HttpPost("usuario")]
        public  async Task<object> Registro([FromBody] Usuario userModel)
        {
            object response = null;
            IEnumerable<Usuario> usuario = null;

            if (userModel == null)
            {
                _log.Error(nameof(userModel).ToString() + "Cannot be null.");
                throw new ArgumentNullException(nameof(userModel), "Cannot be null.");
            }


            try
            {
                response = await _registro.Registro(userModel);

                _log.Information("Response from Registro");

                if (Convert.ToBoolean(response))
                {
                    //usuario = await _login.Login(userModel);

                    // call send email api

                    //response = EnviarCorreo(userModel.email);
                }
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


        internal async Task<object> EnviarCorreo(string email)
        {
            object ret = null;

            if (string.IsNullOrEmpty(email))
            {
                _log.Error(nameof(email).ToString() + "Cannot be null.");
                throw new ArgumentNullException(nameof(email), "Cannot be null.");
            }

            try
            {
                HttpResponseMessage response = await

                client.PostAsJsonAsync("https://localhost:5002/api/correo/enviar", email);
                response.EnsureSuccessStatusCode();
                ret = 1;

                _log.Information("Response from EnviarCorreo");
            }
            catch(Exception e)
            {
                _log.Error(e.ToString());

                ret = 1;
            }

            return ret;
        }
    }
}
