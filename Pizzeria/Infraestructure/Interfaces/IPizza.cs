using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pizzeria.Models;
namespace Pizzeria.Infraestructure.Interfaces
{
    public interface IPizza
    {
        Task<List<Pizza>> GetPizzas();
        Task<Pizza> GetPizzaById(int id);
        Task CreatePizza(Pizza pizza);
        Task UpdatePizza(Pizza pizza);
        Task DeletePizza(Pizza pizza);

        bool PizzaExists(int id);
    }
}
