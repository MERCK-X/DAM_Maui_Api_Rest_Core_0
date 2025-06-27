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
        [HttpGet]
        public List<PersonaCLS> listarPersona()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToString("yyyy-MM-dd"),
                                 fotocadena = persona.Varchivo == null ? "" : "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) + ";base64," + Convert.ToBase64String(persona.Varchivo),
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
        [HttpGet("{nombrecompleto}")]
        public List<PersonaCLS> buscarPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1 && (persona.Nombre + "" + persona.Appaterno + " " + persona.Apmaterno).Contains(nombrecompleto)
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToShortDateString(),
                                 fotocadena = persona.Varchivo == null ? "" : "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) + ";base64," + Convert.ToBase64String(persona.Varchivo),
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }

        }

        // GET recuperarPersona por ID api/Persona/recuperarPersona/{id}-----------------------------------------------------------------------
        [HttpGet("recuperarPersona/{id}")]
        public PersonaCLS recuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    oPersonaCLS = (from persona in bd.Personas
                                   where persona.Bhabilitado == 1
                                   && persona.Iidpersona == id
                                   select new PersonaCLS
                                   {
                                       iidpersona = persona.Iidpersona,
                                       nombre = persona.Nombre,
                                       appaterno = persona.Appaterno,
                                       apmaterno = persona.Apmaterno,
                                       correo = persona.Correo,
                                       fechanacimiento = (DateTime)persona.Fechanacimiento,
                                       fechanacimientocadena = persona.Fechanacimiento == null ? "" : persona.Fechanacimiento.Value.ToString("yyyy-MM-dd"),
                                       iidsexo = (int)persona.Iidsexo,

                                       fotocadena = persona.Varchivo == null ? "" : "data:image/"+System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) + ";base64,"+Convert.ToBase64String(persona.Varchivo),
                                   }).First();
                }
                return oPersonaCLS;
            }
            catch (Exception ex)
            {
                return oPersonaCLS;
            }
        }

        // Delete api/Perssona/eliminarPersona-----------------------------------------------------------------------
        [HttpDelete("{id}")]
        public int eliminarPersona(int id)
        {
            // 0 indica un error y 1 indica exito
            int respuesta = 0;

            try
            {          
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    Persona oPersona = bd.Personas.Where(p => p.Iidpersona == id).First(); // Recuperamos la persona por ID usando una funcion lamda 
                    oPersona.Bhabilitado = 0; // Cambiamos el estado a no habilitado
                    bd.SaveChanges();
                    respuesta = 1; // Operación exitosa
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                return respuesta;
            }
        }

        // Post api/Persona/guardarPersona-----------------------------------------------------------------------
        [HttpPost]
        public int guardarPersona([FromBody] PersonaCLS oPersonaCLS)
        {
            int respuesta = 0;

            try
            {
                int id = oPersonaCLS.iidpersona;
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    if (id == 0) // Si el ID es 0, significa que es una nueva persona
                    {
                        Persona oPersona = new Persona();

                        oPersona.Nombre = oPersonaCLS.nombre;
                        oPersona.Appaterno = oPersonaCLS.appaterno;
                        oPersona.Apmaterno = oPersonaCLS.apmaterno;
                        oPersona.Correo = oPersonaCLS.correo;
                        oPersona.Fechanacimiento = DateTime.Parse(oPersonaCLS.fechanacimientocadena);
                        oPersona.Iidsexo = oPersonaCLS.iidsexo;
                        oPersona.Btieneusuario = 0; // Por defecto, no tiene usuario

                        if (oPersonaCLS.nombrearchivo != "")
                        {
                            oPersona.Vnombrearchivo = oPersonaCLS.nombrearchivo;
                            oPersona.Varchivo = oPersonaCLS.archivo; 
                        }
                        oPersona.Bhabilitado = 1; // Habilitado por defecto
                        bd.Personas.Add(oPersona); // Agregamos la nueva persona a la base de datos
                        bd.SaveChanges();
                        respuesta = 1; // Operación exitosa
                    }
                    else // Si el ID no es 0, significa que es una actualización/editar
                    {
                        Persona oPersona = bd.Personas.Where(p => p.Iidpersona == id).First();

                        oPersona.Nombre = oPersonaCLS.nombre;
                        oPersona.Appaterno = oPersonaCLS.appaterno;
                        oPersona.Apmaterno = oPersonaCLS.apmaterno;
                        oPersona.Correo = oPersonaCLS.correo;
                        oPersona.Fechanacimiento = DateTime.Parse(oPersonaCLS.fechanacimientocadena);
                        oPersona.Iidsexo = oPersonaCLS.iidsexo;
                        if (oPersonaCLS.nombrearchivo != "")
                        {
                            oPersona.Vnombrearchivo = oPersonaCLS.nombrearchivo;
                            oPersona.Varchivo = oPersonaCLS.archivo;
                        }
                        bd.SaveChanges();
                        respuesta = 1; // Operación exitosa
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                return respuesta;
            }
        }


        // GET: api/Persona/listarPersona-----------------------------------------------------------------------
        [HttpGet("listarPersonaSinUsuario")]
        public List<PersonaCLS> listarPersonaSinUsuario()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();
            try
            {
                using (DbAbaf8dBdveterinariaContext bd = new DbAbaf8dBdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             && (persona.Btieneusuario == 0 || persona.Btieneusuario == null)
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
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
