using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface IProducto
    {
        Task<object>  GuardarProducto(Producto producto);

        Task<object> EditarProducto(Producto producto);

        Task<IEnumerable<Producto>> ObtenerProducto(Negocio negocio);

        Task<IEnumerable<Producto>> BorrarProducto(Producto negocio);

        Task<IEnumerable<CatProducto>> ObtnerCatProductoPorIdCatNegocio(string categoria);
    }
}
