using CapaEntidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        // GET: api/Persona/listarPersona-----------------------------------------------------------------------
        [HttpGet]
        public List<TipoUsuarioCLS> listarTipoUsuario()
        {
            List<TipoUsuarioCLS> lista = new List<TipoUsuarioCLS>();
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    lista = (from tipousuario in bd.TipoUsuarios
                             where tipousuario.Bhabilitado == 1
                             select new TipoUsuarioCLS
                             {
                                iidtipousuario = tipousuario.Iidtipousuario,
                                nombretipousuario = tipousuario.Nombre
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }
    }
}
