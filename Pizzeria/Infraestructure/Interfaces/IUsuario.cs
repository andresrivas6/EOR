using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Infraestructure.Interfaces
{
    public interface IUsuario
    {
        int LoginUser(string usuario, string clave);
    }
}
