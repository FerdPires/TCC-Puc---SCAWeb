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
        public IList<ManutencaoEntity> GetAll(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("abertas")]
        [HttpGet]
        public IList<ManutencaoEntity> GetAllIniciada(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAllIniciada();
        }

        [Route("concluidas")]
        [HttpGet]
        public IList<ManutencaoEntity> GetAllConcluida(
            [FromServices] IManutencaoRepository repository
        )
        {
            return repository.GetAllConcluida();
        }

        [Route("agendamento/listar")]
        [HttpGet]
        public IList<AgendaManutencaoEntity> GetAll(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("agendamento/hoje")]
        [HttpGet]
        public IList<AgendaManutencaoEntity> GetAllToday(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllToday();
        }

        [Route("agendamento/abertas")]
        [HttpGet]
        public IList<AgendaManutencaoEntity> GetAllAberto(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllAberto();
        }

        [Route("agendamento/fechadas")]
        [HttpGet]
        public IList<AgendaManutencaoEntity> GetAllFechado(
            [FromServices] IAgendaManutencaoRepository repository
        )
        {
            return repository.GetAllFechado();
        }
    }
}
