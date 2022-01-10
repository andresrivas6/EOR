using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Pizzeria.Infraestructure.Interfaces;
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
            return View();
        }

        public IActionResult Login(Usuario us)
        {
            int valido = _usuario.LoginUser(us.User, us.Pass);
            if (valido != 0)
                return RedirectToAction("Welcome");
            else
            {
                //Usuario usuario = new Usuario();
                ViewBag.errMsj = "Las credenciales ingresadas son invalidas";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Welcome(Usuario us)
        {
            int valido = _usuario.LoginUser(us.User, us.Pass);
            if (valido != 0)
                return RedirectToAction("Welcome");
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
