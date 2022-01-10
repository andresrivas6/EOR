using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pizzeria.Infraestructure.Interfaces;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class InicioController : Controller
    {
        
        private readonly IUsuario _usuario;

        public InicioController(IUsuario usuario)
        {
            _usuario = usuario;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            Usuario u = new Usuario();
            u = _usuario.GetUserByID(id);
            HttpContext.Session.SetInt32("admin", Convert.ToInt32(u.EsAdmin));
            ViewData["admin"] = HttpContext.Session.GetInt32("admin");
            return View();
        }
    }
}
