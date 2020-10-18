using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pedido.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Item 1", "Item 2" };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [Route("authenticated")]
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Authenticated()
        {
            return new[] { "Autenticado - " + User.Identity.Name };
        }

        //=> String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "admin,user")]
        public IEnumerable<string> Employee()
        {
            return new[] { "Funcionário - " + User.Identity.Name };
        }

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Manager()
        {
            return new[] { "Gerente - " + User.Identity.Name };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
