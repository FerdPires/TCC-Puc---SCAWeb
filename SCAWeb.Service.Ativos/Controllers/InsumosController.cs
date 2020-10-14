using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SCAWeb.Service.Ativos.Controllers
{
    public class InsumosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
