using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pizzeria.Models;

namespace Pizzeria.Infraestructure.Interfaces
{
    public interface IOrden
    {
        Task<List<Orden>> GetOrdenes();
        Task<Orden> GetOrdenById(Int64 id);
        Task CreateOrden(Orden orden);
        Task UpdateOrden(Orden orden);
        Task DeleteOrden(Orden orden);

        bool OrdenExists(Int64 id);
    }
}
