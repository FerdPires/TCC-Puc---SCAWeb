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
    [Route("v1/manutencao-corretiva")]
    [Authorize]
    public class ManutencaoCorretivaController : ControllerBase
    {
        [Route("editar")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IServiceActionResult UpdateCorretiva(
            [FromBody] ManutencaoEntity manutencao,
            [FromServices] IManutencaoService service
        )
        {
            //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.ManutencaoCorretivaUpdate(manutencao);
        }

        [Route("agendamento")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IServiceActionResult Agendar(
            [FromBody] AgendaManutencaoEntity agendaManut,
            [FromServices] IAgendaManutencaoService service
        )
        {
            //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.AgendaManutencaoCorretiva(agendaManut);
        }

        [Route("agendamento/editar")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IServiceActionResult UpdateAgendar(
           [FromBody] AgendaManutencaoEntity agendaManut,
           [FromServices] IAgendaManutencaoService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.UpdateAgendaManutencaoCorretiva(agendaManut);
        }

        [Route("agendamento/excluir")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IServiceActionResult DeleteAgendar(
           [FromBody] AgendaManutencaoEntity agendaManut,
           [FromServices] IAgendaManutencaoService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.DeleteAgendaManutencaoCorretiva(agendaManut);
        }

        [Route("agendamento/listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AgendaManutencaoEntity> GetAllCorretiva(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllCorretiva();
        }

        [Route("listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<ManutencaoEntity> GetAllCorretiva(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAllCorretiva();
        }
    }
}
