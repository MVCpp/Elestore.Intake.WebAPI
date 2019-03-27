﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface IDataAccess
    {
        Task<Usuario> Registro(Usuario usuario);

        Task<object> RecuperarCuenta(Usuario usuario);

        Task<IEnumerable<Usuario>> Login(Usuario usuario);

        Task<object> AgregarNegocio(Negocio negocio);

        Task<IEnumerable<Negocio>> ObtenerNegocio(string clientid);

        Task<object> Actualizar(Usuario usuario);

        Task<IEnumerable<CatNegocio>> ObtenerCatNegocio();

        Task<IEnumerable<SubCatNegocio>> ObtenerSubCatNegocio(int id);
    }
}
