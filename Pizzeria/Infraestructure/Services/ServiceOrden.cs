using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Infraestructure.Interfaces;
using Pizzeria.Models;
namespace Pizzeria.Infraestructure.Services
{
    public class ServiceOrden : IOrden
    {
        public async Task<List<Orden>> GetOrdenes()
        {
            try
            {
                List<Orden> ordenes = null;

                using (var context = new EorContext())
                {
                    ordenes = await context.Orden
                                            .Include(p => p.Pizza)
                                            .Include(u => u.Usuario)
                                            .ToListAsync();
                }

                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Orden> GetOrdenById(Int64 id)
        {
            try
            {
                var orden = new Orden();

                using (var context = new EorContext())
                {
                    orden = await context.Orden.Where(o=> o.Id==id)
                                                .Include(u => u.Usuario)
                                                .Include(p => p.Pizza).FirstOrDefaultAsync();
                }

                return orden;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task CreateOrden(Orden orden)
        {
            var response = 0;
            try
            {
                if (orden == null)
                {
                    throw new ArgumentNullException(nameof(Orden));
                }


                using (var context = new EorContext())
                {
                    await context.Orden.AddAsync(orden);
                    response = await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteOrden(Orden orden)
        {
            var response = 0;
            try
            {
                if (orden == null)
                {
                    throw new ArgumentNullException(nameof(Orden));
                }

                using (var context = new EorContext())
                {
                    context.Orden.Remove(orden);
                    response = await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return response;
        }



        public async Task UpdateOrden(Orden orden)
        {
            var response = 0;
            try
            {
                if (orden == null)
                {
                    throw new ArgumentNullException(nameof(orden));
                }

                using (var context = new EorContext())
                {
                    context.Entry(orden).State = EntityState.Modified;
                    response = await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return response;
        }

        public bool OrdenExists(Int64 id)
        {
            using (var context = new EorContext())
            {
                return context.Orden.Any(e => e.Id == id);
            }
        }
    }
}
