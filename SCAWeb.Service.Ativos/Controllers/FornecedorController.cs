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
    [Route("v1/fornecedor")]
    [Authorize]
    public class FornecedorController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public IServiceActionResult Create(
            [FromBody] FornecedorEntity fornecedor,
            [FromServices] IFornecedorService service
        )
        {
            //  command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.CreateFornecedor(fornecedor);
        }

        [Route("")]
        [HttpPut]
        public IServiceActionResult Update(
           [FromBody] FornecedorEntity fornecedor,
           [FromServices] IFornecedorService service
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.UpdateFornecedor(fornecedor);
        }

        [Route("{id}")]
        [HttpDelete]
        public IServiceActionResult Delete(
            Guid id,
           [FromServices] IFornecedorService service
        )
        {
            //  var insumo = new InsumoEntity();

            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (ServiceActionResult)service.DeleteFornecedor(id);
        }

        [Route("all")]
        [HttpGet]
        public IList<FornecedorEntity> GetAll(
            [FromServices] IFornecedorRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("{id}")]
        [HttpGet]
        public FornecedorEntity GetById(
            Guid id,
            [FromServices] IFornecedorRepository repository
        )
        {
            return repository.GetById(id);
        }

        [Route("{cnpj}")]
        [HttpGet]
        public FornecedorEntity GetByCnoj(
            int cnpj,
            [FromServices] IFornecedorRepository repository
        )
        {
            return repository.GetByCnpj(cnpj);
        }
    }
}
