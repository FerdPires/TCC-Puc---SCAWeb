using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAWeb.Service.Monitoramento.Entities;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Monitoramento.Controllers
{
    [ApiController]
    [Route("api/area-risco")]
    [Authorize]
    public class AreaController : ControllerBase
    {
        [Route("listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AreaEntity> GetAll(
            [FromServices] IAreaRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("obter/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public AreaEntity GetById(
            Guid id,
            [FromServices] IAreaRepository repository
        )
        {
            return repository.GetById(id);
        }
    }
}
