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
    [Route("v1/tipo-insumo")]
    [Authorize]
    public class TipoInsumoController : ControllerBase
    {
        [Route("criar")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IServiceActionResult Create(
            [FromBody] TipoInsumoEntity tipoInsumo,
            [FromServices] ITipoInsumoService service
        )
        {
            tipoInsumo.user = User.Identity.Name;
            return (ServiceActionResult)service.CreateTipoInsumo(tipoInsumo);
        }

        [Route("editar")]
        [HttpPut]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin,user")]
        public IList<TipoInsumoEntity> GetAll(
            [FromServices] ITipoInsumoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("obter/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public TipoInsumoEntity GetById(
            Guid id,
            [FromServices] ITipoInsumoRepository repository
        )
        {
            return repository.GetById(id);
        }
    }
}
