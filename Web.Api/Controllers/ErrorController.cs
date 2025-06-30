using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public string ErrorAPI()
        {
            return "YA VALIOO, NO TIENE PERMISO PARA ACCEDER A LA API JAJA";
        }
    }
}
