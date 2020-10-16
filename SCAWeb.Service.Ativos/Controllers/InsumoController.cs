﻿using Microsoft.AspNetCore.Authorization;
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
        [Route("")]
        [HttpPost]
        public IServiceActionResult Create(
            [FromBody] InsumoEntity insumo, 
            [FromServices] IInsumoService service
        )
        {
          //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.CreateInsumo(insumo);
        }

        [Route("")]
        [HttpPut]
        public IServiceActionResult Update(
           [FromBody] InsumoEntity insumo,
           [FromServices] IInsumoService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.UpdateInsumo(insumo);
        }

        [Route("{id}")]
        [HttpDelete]
        public IServiceActionResult Delete(
            Guid id,
           [FromServices] IInsumoService service
        )
        {
          //  var insumo = new InsumoEntity();

            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.DeleteInsumo(id);
        }

        [Route("all")]
        [HttpGet]
        public IList<InsumoEntity> GetAll(
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("{id}")]
        [HttpGet]
        public InsumoEntity GetById(
            Guid id,
            [FromServices] IInsumoRepository repository
        )
        {
            return repository.GetById(id);
        }
    }
}