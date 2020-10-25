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
    [Route("v1/manutencao")]
    [Authorize]
    public class ManutencaoController : ControllerBase
    {
        [Route("criar")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IServiceActionResult Create(
            [FromBody] ManutencaoEntity manutencao,
            [FromServices] IManutencaoService service
        )
        {
            //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.ManutencaoCreate(manutencao);
        }

        [Route("listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<ManutencaoEntity> GetAll(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("abertas")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<ManutencaoEntity> GetAllIniciada(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAllIniciada();
        }

        [Route("concluidas")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<ManutencaoEntity> GetAllConcluida(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAllConcluida();
        }

        [Route("agendamento/listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AgendaManutencaoEntity> GetAll(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("agendamento/hoje")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AgendaManutencaoEntity> GetAllUntilToday(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllUntilToday();
        }

        [Route("agendamento/abertas")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AgendaManutencaoEntity> GetAllAberto(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllAberto();
        }

        [Route("agendamento/fechadas")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AgendaManutencaoEntity> GetAllFechado(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllFechado();
        }
    }
}
