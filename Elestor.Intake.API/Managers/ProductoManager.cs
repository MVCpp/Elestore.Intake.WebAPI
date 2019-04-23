using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Managers
{
    public class ProductoManager: IProducto
    {
        readonly IDataAccess _dataAccess;

        public ProductoManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<object> GuardarProducto(Producto producto)
        {
            object response = null;
            try
            {
                response = await _dataAccess.GuardarProducto(producto);

            }
            catch (Exception ex)
            {
                response = ex;
            }

            return response;
        }

        public async Task<IEnumerable<Producto>> ObtenerProducto(string negocioid)
        {
            IEnumerable<Producto> response = null;

            try
            {
                response = await _dataAccess.ObtenerProductos(negocioid);

            }
            catch (Exception ex)
            {
                response = null;
            }

            return response;
        }
    }
}
