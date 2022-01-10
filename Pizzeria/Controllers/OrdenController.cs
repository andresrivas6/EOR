using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.Infraestructure.Interfaces;
namespace Pizzeria.Controllers
{
    public class OrdenController : Controller
    {
        private readonly EorContext _context;
        private readonly IUsuario _usuario;
        public OrdenController(EorContext context, IUsuario usuario)
        {
            _context = context;
            _usuario = usuario;
        }

        // GET: Orden
        public async Task<IActionResult> Index()
        {
            Usuario u = new Usuario();
            int id;
            if (HttpContext.Session.GetInt32("usuario") == null)
                id = 0;
            else
                id = HttpContext.Session.GetInt32("usuario").Value;


            u = _usuario.GetUserByID(id);
            
            ViewData["admin"] = HttpContext.Session.GetInt32("admin");

            IEnumerable<Orden> lstOrden;

            if (u.EsAdmin)
                lstOrden = await _context.Orden.Where(o => o.Entregada == false).ToListAsync();
            else
                lstOrden = await _context.Orden.Where(o => o.UsuarioIngreso == u.Id && o.Entregada == false).ToListAsync();

            return View(lstOrden);
        }

        // GET: Orden/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Orden/Create
        public IActionResult Create()
        {
            var pizzas = _context.Pizza.ToList();
            List<SelectListItem> ps = new List<SelectListItem>();
            foreach(var item in pizzas)
            {
                ps.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.NombreProducto
                });
            }

            ViewBag.Pizzas = ps;

            return View();
        }

        // POST: Orden/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreSolicitante,CantOrden,Direccion,Comentarios,Entregada,Total,Fecha")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // GET: Orden/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return View(orden);
        }

        // POST: Orden/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            _context.Orden.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return _context.Orden.Any(e => e.Id == id);
        }
    }
}
