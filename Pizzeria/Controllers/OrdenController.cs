using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.Infraestructure.Interfaces;
namespace Pizzeria.Controllers
{
    public class OrdenController : Controller
    {
        //private readonly EorContext _context;
        private readonly IUsuario _usuario;
        private readonly IOrden _orden;
        private readonly IPizza _pizza;
        public OrdenController(EorContext context, IUsuario usuario, IOrden orden, IPizza pizza)
        {
            //_context = context;
            _usuario = usuario;
            _orden = orden;
            _pizza = pizza;
        }

        // GET: Orden
        public async Task<IActionResult> Index()
        {
            int id;
            if (HttpContext.Session.GetInt32("usuario") == null)
                id = 0;
            else
                id = HttpContext.Session.GetInt32("usuario").Value;

            ViewData["admin"] = HttpContext.Session.GetInt32("admin");

            IEnumerable<Orden> lstOrden;

            if (Convert.ToBoolean(HttpContext.Session.GetInt32("admin")))
            {
                lstOrden = await _orden.GetOrdenes();
                lstOrden = lstOrden.Where(o => o.Entregada == false).OrderBy(o => o.Fecha);
            }
            else
            {
                lstOrden = await _orden.GetOrdenes();
                lstOrden = lstOrden.Where(o => o.UsuarioIngreso == id && o.Entregada == false).OrderBy(o => o.Fecha);
            }

            return View(lstOrden);
            //Usuario u = new Usuario();
            //int id;
            //if (HttpContext.Session.GetInt32("usuario") == null)
            //    id = 0;
            //else
            //    id = HttpContext.Session.GetInt32("usuario").Value;


            //u = _usuario.GetUserByID(id);

            //ViewData["admin"] = HttpContext.Session.GetInt32("admin");

            //IEnumerable<Orden> lstOrden;

            //if (u.EsAdmin)
            //    lstOrden = await _context.Orden.Where(o => o.Entregada == false)
            //                    .Include(p => p.Pizza)
            //                    .Include(u => u.Usuario)
            //                    .ToListAsync();
            //else
            //    lstOrden = await _context.Orden.Where(o => o.UsuarioIngreso == u.Id && o.Entregada == false)
            //                                    .Include(p => p.Pizza)
            //                                    .Include(u => u.Usuario)
            //                                    .ToListAsync();

            //return View(lstOrden);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var orden = await _context.Orden.FirstOrDefaultAsync(m => m.Id == id);
            var orden = await _orden.GetOrdenById(id.Value);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        
        public async Task<IActionResult> Create()
        {
            //var pizzas = _context.Pizza.ToList();
            List<Pizza> pizzas = await _pizza.GetPizzas();
            List<SelectListItem> ps = new List<SelectListItem>();
            foreach(var item in pizzas)
            {
                ps.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.NombreProducto+" - "+item.Tamano
                });
            }

            ViewBag.Pizzas = ps;

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreSolicitante,CantOrden,Direccion,Comentarios,Entregada,Total,Fecha")] Orden orden)
        {
            try
            {
                orden.Fecha = DateTime.Now;
                //Usuario u = _context.Usuario.Find(HttpContext.Session.GetInt32("usuario").Value);
                //orden.UsuarioIngreso = u.Id;
                //orden.Usuario = u;
                orden.UsuarioIngreso = Convert.ToInt32(HttpContext.Session.GetInt32("usuario").ToString());

                //Pizza p = _context.Pizza.Find(Convert.ToInt32(Request.Form["TipoPizza"].ToString()));
                //orden.TipoPizza = p.Id;
                //orden.Pizza = p;
                orden.TipoPizza = Convert.ToInt32(Request.Form["TipoPizza"].ToString());

                if (ModelState.IsValid)
                {
                    //_context.Add(orden);
                    //await _context.SaveChangesAsync();
                    await _orden.CreateOrden(orden);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //var pizzas = _context.Pizza.ToList();
            var pizzas = await _pizza.GetPizzas();
            List<SelectListItem> ps = new List<SelectListItem>();
            foreach (var item in pizzas)
            {
                ps.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.NombreProducto + " - " + item.Tamano
                });
            }

            ViewBag.Pizzas = ps;
            return View(orden);
        }

        
        public async Task<IActionResult> Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var orden = await _context.Orden
            //                            .Where(o => o.Id==id)
            //                            .Include(p => p.Pizza)
            //                            .FirstOrDefaultAsync();
            var orden = await _orden.GetOrdenById(id.Value);
            if (orden == null)
            {
                return NotFound();
            }

            //var pizzas = _context.Pizza.ToList();
            var pizzas = await _pizza.GetPizzas();
            List<SelectListItem> ps = new List<SelectListItem>();
            foreach (var item in pizzas)
            {
                ps.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.NombreProducto + " - " + item.Tamano
                });
            }
            
            ViewBag.Pizzas = ps;

            return View(orden);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreSolicitante,CantOrden,Direccion,Comentarios,Entregada,Total,Fecha")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Usuario u = _context.Usuario.Find(HttpContext.Session.GetInt32("usuario").Value);
                    //orden.UsuarioIngreso = u.Id;
                    //orden.Usuario = u;
                    var or = await _orden.GetOrdenById(id);

                    //Usuario u = _usuario.GetUserByID(or.UsuarioIngreso);
                    orden.UsuarioIngreso = or.Usuario.Id;
                    orden.Usuario = or.Usuario;

                    //Pizza p = _context.Pizza.Find(Convert.ToInt32(Request.Form["TipoPizza"].ToString()));
                    var p = await _pizza.GetPizzaById(Convert.ToInt32(Request.Form["TipoPizza"].ToString()));
                    orden.TipoPizza = p.Id;
                    orden.Pizza = p;
                    //orden.TipoPizza = p.Id;
                    //orden.Pizza = p;

                    //_context.Update(orden);
                    //await _context.SaveChangesAsync();
                    await _orden.UpdateOrden(orden);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
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
            return View(orden);
        }

        // GET: Orden/Delete/5
        public async Task<IActionResult> Delete(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var orden = await _context.Orden
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var orden = await _orden.GetOrdenById(id.Value);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int64 id)
        {
            //var orden = await _context.Orden.FindAsync(id);
            //_context.Orden.Remove(orden);
            //await _context.SaveChangesAsync();
            var orden = await _orden.GetOrdenById(id);
            await _orden.DeleteOrden(orden);

            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(Int64 id)
        {
            //return _context.Orden.Any(e => e.Id == id);
            return _orden.OrdenExists(id);
        }

        
        public async Task<Decimal> Precio(int idPizza)
        {

            //return _context.Pizza.Find(idPizza).Precio;
            Pizza p = await _pizza.GetPizzaById(idPizza);
            return p.Precio;
        }
    }
}
