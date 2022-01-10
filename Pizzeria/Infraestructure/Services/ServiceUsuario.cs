﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.Infraestructure.Interfaces;

namespace Pizzeria.Infraestructure.Services
{
    public class ServiceUsuario : IUsuario
    {
        public int LoginUser(string usuario, string clave)
        {
            int res = 0;
            try
            {
                using (var _context = new EorContext())
                {
                    Usuario usu = new Usuario();

                    //usu = _context.Usuario.FromSql(@"EXECUTE dbo.GetProductByCategory {0},{1}
                    //", new Array[usuario, clave]);

                    var connection = (SqlConnection)_context.Database.GetDbConnection();

                    DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();

                    cmd.CommandText = "dbo.verificarUsuario";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@pUsuario", SqlDbType.VarChar) { Value = usuario });
                    cmd.Parameters.Add(new SqlParameter("@pContra", SqlDbType.VarChar) { Value = clave });
                    SqlParameter valido = new SqlParameter("@pValido", SqlDbType.Bit);
                    valido.Direction = ParameterDirection.Output;
                    SqlParameter errUsuario = new SqlParameter("@pErrorUsuario", SqlDbType.Bit);
                    errUsuario.Direction = ParameterDirection.Output;
                    SqlParameter errTecnico = new SqlParameter("@pErrorTecnico", SqlDbType.Bit);
                    errTecnico.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(valido);
                    cmd.Parameters.Add(errUsuario);
                    cmd.Parameters.Add(errTecnico);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    res = Convert.ToInt32(valido.Value);
                }

                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
