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
    [Route("api/fornecedor")]
    [Authorize]
    public class FornecedorController : ControllerBase
    {
        [Route("criar")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IServiceActionResult Create(
            [FromBody] FornecedorEntity fornecedor,
            [FromServices] IFornecedorService service
        )
        {
            fornecedor.user = User.Identity.Name;
            return (ServiceActionResult)service.CreateFornecedor(fornecedor);
        }

        [Route("editar")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IServiceActionResult Update(
           [FromBody] FornecedorEntity fornecedor,
           [FromServices] IFornecedorService service
        )
        {
            fornecedor.user = User.Identity.Name;
            return (ServiceActionResult)service.UpdateFornecedor(fornecedor);
        }

        [Route("excluir/{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IServiceActionResult Delete(
            Guid id,
           [FromServices] IFornecedorService service
        )
        {
            ///fornecedor.user = User.Identity.Name;
            return (ServiceActionResult)service.DeleteFornecedor(id);
        }

        [Route("listar")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IList<FornecedorEntity> GetAll(
            [FromServices] IFornecedorRepository repository
        )
        {
            return repository.GetAll();
        }

        [Route("obter/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public FornecedorEntity GetById(
            Guid id,
            [FromServices] IFornecedorRepository repository
        )
        {
            return repository.GetById(id);
        }

        [Route("obter/{cnpj}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public FornecedorEntity GetByCnpj(
            string cnpj,
            [FromServices] IFornecedorRepository repository
        )
        {
            return repository.GetByCnpj(cnpj);
        }
    }
}
