using Microsoft.AspNetCore.Http;
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
    public class UsuarioController : ControllerBase
    {
        // GET: api/Usuario/listarUsuario-----------------------------------------------------------------------
        [HttpGet]
        [ServiceFilter(typeof(Seguridad))]
        public List<UsuarioCLS> listarUsuario()
        {
            List<UsuarioCLS> lista = new List<UsuarioCLS>();
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    lista = (from usuario in bd.Usuarios
                             join persona in bd.Personas on usuario.Iidpersona equals persona.Iidpersona
                             join tipousuario in bd.TipoUsuarios on usuario.Iidtipousuario equals tipousuario.Iidtipousuario
                             where usuario.Bhabilitado == 1
                             select new UsuarioCLS
                             {
                                 iidusuario = usuario.Iidusuario,
                                 nombreusuario = usuario.Nombreusuario.ToLower(),
                                 nombrepersona = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 iidtipousuario = (int)usuario.Iidtipousuario,
                                 nombretipousuario = tipousuario.Nombre,
                                 fotopersona = persona.Varchivo == null ? "" : "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) + ";base64," + Convert.ToBase64String(persona.Varchivo),
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        // GET NOMBRE api/Persona/listarPersona-----------------------------------------------------------------------
        [HttpPost]
        [ServiceFilter(typeof(Seguridad))]
        public List<UsuarioCLS> buscarUsuarios([FromBody] UsuarioCLS oUsuarioCLS)
        {
            string nombreusuario = oUsuarioCLS.nombreusuario;
            int iidtipousuario = oUsuarioCLS.iidtipousuario;

            List<UsuarioCLS> lista = listarUsuario();

            if (nombreusuario != "")
            {
                lista = lista.Where(p => p.nombreusuario.Contains(nombreusuario)).ToList();
            }
            if (iidtipousuario != 0)
            {
                lista = lista.Where(p => p.iidtipousuario == iidtipousuario).ToList();
            }

            return lista;

        }

        //Metodo para guardar datos
        //-> Insertar en la tabla de usuarios(INSERT usuarios)
        //-> Cambiar Btieneusuario = 0 -> Btieneusuario = 1 (UPDATE persona)

        [HttpPost("guardarDatos")]
        [ServiceFilter(typeof(Seguridad))]
        public int guardarDatos([FromBody] UsuarioCLS oUsuarioCLS)
        {
            int resultado = 0;
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                    {
                        //nuevo usuario
                        if (oUsuarioCLS.iidusuario == 0)
                        {
                            Usuario oUsuario = new Usuario();
                            oUsuario.Nombreusuario = oUsuarioCLS.nombreusuario;
                            oUsuario.Iidtipousuario = oUsuarioCLS.iidtipousuario;
                            oUsuario.Iidpersona = oUsuarioCLS.iidpersona;
                            //oUsuario.Contra = Utils.cifrarCadena(oUsuarioCLS.contra);
                            oUsuario.Contra = Utils.cifrarCadena("1234");

                            oUsuario.Bhabilitado = 1; //Activo       
                            bd.Usuarios.Add(oUsuario);
                            bd.SaveChanges();

                            Persona oPersona = bd.Personas.Where(p => p.Iidpersona == oUsuarioCLS.iidpersona).FirstOrDefault();
                            oPersona.Btieneusuario = 1; //Cambiar Btieneusuario = 0 -> Btieneusuario = 1
                            bd.SaveChanges();
                            transaction.Complete();
                            resultado = 1;
                        }
                        else
                        {
                            //Actualizar usuario
                            Usuario oUsuario = bd.Usuarios.Where(p => p.Iidusuario == oUsuarioCLS.iidusuario).FirstOrDefault();

                            oUsuario.Nombreusuario = oUsuarioCLS.nombreusuario;
                            oUsuario.Iidtipousuario = oUsuarioCLS.iidtipousuario;

                            bd.SaveChanges();
                            transaction.Complete();
                            resultado = 1;
                        }
                    }
                }
                return resultado; //Insertar
            }
            catch (Exception ex)
            {
                return resultado;
            }
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(Seguridad))]
        public UsuarioCLS recuperarUsuario(int id)
        {
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    oUsuarioCLS = (from usuario in bd.Usuarios
                                   where usuario.Iidusuario == id 
                                   select new UsuarioCLS
                                   {
                                       iidusuario = usuario.Iidusuario,
                                       nombreusuario = usuario.Nombreusuario,
                                       iidtipousuario = (int)usuario.Iidtipousuario   
                                   }).First();
                    return oUsuarioCLS;
                }
            }
            catch (Exception ex)
            {
                return oUsuarioCLS;
            }
        }

        //Metodo para eliminar usuario
        //1. -> Cambiar en la tabla usuario Bhabilitado de 1 a 0
        //2. -> Cambiar en la tabla Persona Btieneusuario de 1 a 0
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(Seguridad))]
        public int eliminarUsuario(int id)
        {
            int respuesta = 0;
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                    {
                        Usuario oUsuario = bd.Usuarios.Where(p => p.Iidusuario == id).First();
                        oUsuario.Bhabilitado = 0; //Cambiar BHabilitado de 1 a 0
                        bd.SaveChanges();

                        Persona oPersona = bd.Personas.Where(p => p.Iidpersona == oUsuario.Iidpersona).First();
                        oPersona.Btieneusuario = 0; //Cambiar BHabilitado de 1 a 0
                        bd.SaveChanges();

                        transaction.Complete();
                        respuesta = 1;
                        return respuesta;
                    }
                }
                
            }
            catch (Exception ex)
            {
                return respuesta;
            }
        }
    }
}
