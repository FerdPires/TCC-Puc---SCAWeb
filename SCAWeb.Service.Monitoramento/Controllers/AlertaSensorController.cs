using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCAWeb.Service.Monitoramento.Entities;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Monitoramento.Controllers
{
    [ApiController]
    [Route("api/alerta")]
    [Authorize]
    public class AlertaSensorController : ControllerBase
    {
        [Route("listar/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<AlertaSensorEntity> GetAllBySensor(
            Guid id,
            [FromServices] IAlertaSensorRepository repository
        )
        {
            return repository.GetAllBySensor(id);
        }

        [Route("obter/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public AlertaSensorEntity GetById(
            Guid id,
            [FromServices] IAlertaSensorRepository repository
        )
        {
            return repository.GetById(id);
        }
    }
}
