using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Interfaces;

namespace SCAWeb.Service.Ativos.Controllers
{
    [ApiController]
    [Route("v1/insumo")]
    [Authorize]
    public class InsumoController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public IServiceActionResult Create([FromBody] InsumoEntity insumo, [FromServices] IInsumoService service)
        {
          //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.CreateInsumo(insumo);
        }
    }
}
