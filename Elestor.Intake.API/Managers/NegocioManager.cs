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
            object response = null;

            try
            {
                response = await _dataAccess.AgregarNegocio(negocio);
            }
            catch(Exception ex)
            {
                response = ex; 
            }

            return response;
        }

        public async Task<IEnumerable<Negocio>> ObtenerNegocio(string clientid)
        {
            IEnumerable<Negocio> response = null;

            try
            {
               response = await _dataAccess.ObtenerNegocio(clientid);
            }
            catch(Exception ex)
            {
               return null; 
            }

            return response;
        }
    }
}
