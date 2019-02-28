using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class NegocioManager : INegocio
    {
        readonly IDataAccess _dataAccess;

        public NegocioManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<object> AgregarNegocio(Negocio negocio)
        {
            return await _dataAccess.AgregarNegocio(negocio);
        }

        public async Task<IEnumerable<Negocio>> ObtenerNegocio(string clientid)
        {
            return await _dataAccess.ObtenerNegocio(clientid);
        }
    }
}
