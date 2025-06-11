using CapaEntidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;


namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        // GET: api/Persona/listarPersona-----------------------------------------------------------------------
        [HttpGet("{nombrecompleto}")]
        public List<PersonaCLS> listarPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAba36cBdveterinariaContext bd = new DbAba36cBdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1 && (persona.Nombre + "" + persona.Appaterno + " " + persona.Apmaterno).Contains(nombrecompleto)
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToShortDateString()
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
