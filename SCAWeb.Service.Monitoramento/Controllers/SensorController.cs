using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAWeb.Service.Monitoramento.Entities;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Monitoramento.Controllers
{
    [ApiController]
    [Route("api/sensor")]
    [Authorize]
    public class SensorController : ControllerBase
    {
        [Route("listar/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IList<SensorEntity> GetAllByArea(
            Guid id,
            [FromServices] ISensorRepository repository
        )
        {
            return repository.GetAllByArea(id);
        }

        [Route("obter/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public SensorEntity GetById(
            Guid id,
            [FromServices] ISensorRepository repository
        )
        {
            return repository.GetById(id);
        }
    }
}
