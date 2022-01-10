using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.Infraestructure.Interfaces;
namespace Pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        //private readonly EorContext _context;
        private readonly IPizza _pizza;
        public PizzaController(EorContext context, IPizza pizza)
        {
            //_context = context;
            _pizza = pizza;
        }

        public async Task<IActionResult> Index()
        {
            Int32? ad = HttpContext.Session.GetInt32("admin");
            ViewData["admin"] = ad;
            List<Pizza> pizzas = await _pizza.GetPizzas();

            return View(pizzas);
            //return View(await _context.Pizza.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var pizza = await _context.Pizza
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var pizza = await _pizza.GetPizzaById(id.Value);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreProducto,Tamano,CantPorciones,Precio,Descripcion")] Pizza pizza)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(pizza);
                    //await _context.SaveChangesAsync();

                    await _pizza.CreatePizza(pizza);

                    return RedirectToAction(nameof(Index));
                }
                return View(pizza);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var pizza = await _context.Pizza.FindAsync(id);
            var pizza = await _pizza.GetPizzaById(id.Value);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreProducto,Tamano,CantPorciones,Precio,Descripcion")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(pizza);
                    //await _context.SaveChangesAsync();
                    await _pizza.UpdatePizza(pizza);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var pizza = await _context.Pizza
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var pizza = await _pizza.GetPizzaById(id.Value);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var pizza = await _context.Pizza.FindAsync(id);
            //_context.Pizza.Remove(pizza);
            //await _context.SaveChangesAsync();

            var pizza = await _pizza.GetPizzaById(id);
            await _pizza.DeletePizza(pizza);

            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _pizza.PizzaExists(id);
        }
    }
}
