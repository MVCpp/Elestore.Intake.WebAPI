﻿using System;
using System.Collections.Generic;

namespace Elestor.Intake.API.Models
{
    public class Usuario
    {
        public Usuario()
        {
           
        }
        public string clientid { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;
        public string nombreUsuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string confirmPassword { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string numeroTelefonico { get; set; } = string.Empty;
        public string fotografia { get; set; } = String.Empty;

        public List<Negocio> negocio { get; set; } = new List<Negocio>();
    }
}
