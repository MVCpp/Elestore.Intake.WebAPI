﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface INegocio
    {
      Task<object> AgregarNegocio(Negocio negocio);

      Task<IEnumerable<Negocio>> ObtenerNegocio(string clientid);
    }
}
