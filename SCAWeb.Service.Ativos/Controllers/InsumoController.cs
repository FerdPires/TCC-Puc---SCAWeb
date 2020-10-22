using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Controllers
{
    [ApiController]
    [Route("v1/insumo")]
    [Authorize]
    public class InsumoController : ControllerBase
    {
        [Route("criar")]
        [HttpPost]
        public IServiceActionResult Create(
            [FromBody] InsumoEntity insumo, 
            [FromServices] IInsumoService service
        )
        {
          //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.CreateInsumo(insumo);
        }

        [Route("editar")]
        [HttpPut]
        public IServiceActionResult Update(
           [FromBody] InsumoEntity insumo,
           [FromServices] IInsumoService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.UpdateInsumo(insumo);
        }

        [Route("excluir")]
        [HttpDelete]
        public IServiceActionResult Delete(
           [FromBody] InsumoEntity insumo,
           [FromServices] IInsumoService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.DeleteInsumo(insumo);
        }

        [Route("listar")]
        [HttpGet]
        public IList<InsumoEntity> GetAll(
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("obter/{id}")]
        [HttpGet]
        public InsumoEntity GetById(
            Guid id,
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetById(id);
        }

        [Route("em-manutencao")]
        [HttpGet]
        public IList<InsumoEntity> GetAllEmManutencao(
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetAllEmManutencao();
        }

        [Route("ativos")]
        [HttpGet]
        public IList<InsumoEntity> GetAllAtivos(
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetAllAtivos();
        }

        [Route("inativos")]
        [HttpGet]
        public IList<InsumoEntity> GetAllInativos(
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetAllInativos();
        }
    }
}
