﻿using System;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface IRegistro
    {
        Task<object> Registro(Usuario usuario);
    }
}
