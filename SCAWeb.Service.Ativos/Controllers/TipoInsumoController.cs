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
    [Route("v1/tipoinsumo")]
    [Authorize]
    public class TipoInsumoController : ControllerBase
    {
        [Route("criar")]
        [HttpPost]
        public IServiceActionResult Create(
            [FromBody] TipoInsumoEntity tipoInsumo,
            [FromServices] ITipoInsumoService service
        )
        {
            //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.CreateTipoInsumo(tipoInsumo);
        }

        [Route("editar")]
        [HttpPut]
        public IServiceActionResult Update(
           [FromBody] TipoInsumoEntity tipoInsumo,
           [FromServices] ITipoInsumoService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.UpdateTipoInsumo(tipoInsumo);
        }

        [Route("excluir/{id}")]
        [HttpDelete]
        public IServiceActionResult Delete(
            Guid id,
           [FromServices] ITipoInsumoService service
        )
        {
            //  var insumo = new InsumoEntity();

            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.DeleteTipoInsumo(id);
        }

        [Route("listar")]
        [HttpGet]
        public IList<TipoInsumoEntity> GetAll(
            [FromServices] ITipoInsumoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("obter/{id}")]
        [HttpGet]
        public TipoInsumoEntity GetById(
            Guid id,
            [FromServices] ITipoInsumoRepository repository
        )
        {
            return repository.GetById(id);
        }
    }
}
