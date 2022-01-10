using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Pizzeria.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuario _usuario;

        public HomeController(ILogger<HomeController> logger, IUsuario usuario)
        {
            _logger = logger;
            _usuario = usuario;
        }

        public IActionResult Index()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Login()
        {
            int valido = _usuario.LoginUser(Request.Form["Usuario"].ToString(), Request.Form["Password"].ToString());
            
            if (valido != 0)
            {
                Usuario u = new Usuario();
                u = _usuario.GetUserByID(valido);

                HttpContext.Session.SetInt32("admin", Convert.ToInt32(u.EsAdmin));
                HttpContext.Session.SetInt32("usuario", Convert.ToInt32(u.Id));

                return RedirectToAction("Index", "Orden");
            }
            else
            {
                //Usuario usuario = new Usuario();
                ViewBag.errMsj = "Las credenciales ingresadas son invalidas";
                return RedirectToAction("Index");
            }
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
