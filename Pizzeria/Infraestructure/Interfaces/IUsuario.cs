using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Pizzeria.Models;
namespace Pizzeria.Infraestructure.Interfaces
{
    public interface IUsuario
    {
        int LoginUser(string usuario, string clave);
        Usuario GetUserByID(int id);
    }
}
