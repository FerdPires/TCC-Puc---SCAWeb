using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Controllers
{
    [ApiController]
    [Route("v1/manutencao-preventiva")]
    [Authorize]
    public class ManutencaoPreventivaController : ControllerBase
    {
        [Route("editar")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IServiceActionResult UpdatePreventiva(
            [FromBody] ManutencaoEntity manutencao,
            [FromServices] IManutencaoService service
        )
        {
            //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.ManutencaoPreventivaUpdate(manutencao);
        }

        [Route("agendamento/listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AgendaManutencaoEntity> GetAllPreventiva(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllPreventiva();
        }

        [Route("listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<ManutencaoEntity> GetAllPreventiva(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAllPreventiva();
        }
    }
}
