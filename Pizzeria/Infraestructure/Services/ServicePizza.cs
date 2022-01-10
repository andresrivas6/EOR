using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Infraestructure.Interfaces;
using Pizzeria.Models;
namespace Pizzeria.Infraestructure.Services
{
    public class ServicePizza : IPizza
    {
        public async Task<List<Pizza>> GetPizzas()
        {
            try
            {
                List<Pizza> pizzas = null;

                using (var context = new EorContext())
                {
                    pizzas = await context.Pizza.ToListAsync();
                }

                return pizzas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pizza> GetPizzaById(int id)
        {
            try
            {
                var pizza = new Pizza();

                using (var context = new EorContext())
                {
                    pizza = await context.Pizza.FindAsync(id);
                }

                return pizza;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task CreatePizza(Pizza pizza)
        {
            var response = 0;
            try
            {
                if (pizza == null)
                {
                    throw new ArgumentNullException(nameof(Pizza));
                }


                using (var context = new EorContext())
                {
                    await context.Pizza.AddAsync(pizza);
                    response = await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletePizza(Pizza pizza)
        {
            var response = 0;
            try
            {
                if (pizza == null)
                {
                    throw new ArgumentNullException(nameof(pizza));
                }

                using (var context = new EorContext())
                {
                    context.Pizza.Remove(pizza);
                    response = await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return response;
        }



        public async Task UpdatePizza(Pizza Pizza)
        {
            var response = 0;
            try
            {
                if (Pizza == null)
                {
                    throw new ArgumentNullException(nameof(Pizza));
                }

                using (var context = new EorContext())
                {
                    context.Entry(Pizza).State = EntityState.Modified;
                    response = await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return response;
        }
    
        public bool PizzaExists(int id)
        {
            using (var context = new EorContext())
            {
                return context.Pizza.Any(e => e.Id == id);
            }
        }
    }
}
