﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using Web.Api.Models;
using System.Transactions;
using Web.Api.Generic;
using Web.Api.Filters;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Metodo de logueo
        [HttpGet("{nombreusuario}/{contra}")]
        public int Login(string nombreusuario, string contra)
        {
            int respuesta = 0;
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                { 
                    string contracifrada = Utils.cifrarCadena(contra);
                    var lista = bd.Usuarios.Where(p => p.Nombreusuario == nombreusuario && p.Contra == contracifrada);
                    int cantidad = lista.Count();

                    if (cantidad == 0)
                    {
                        return respuesta;
                    }
                    else
                    {
                        return lista.First().Iidusuario;
                    }
                }              
            }
            catch(Exception ex)
            {
                return respuesta;
            }
        }

    }
}
