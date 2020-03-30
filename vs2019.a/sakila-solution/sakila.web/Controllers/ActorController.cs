using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace sakila.web.Controllers
{
    public class ActorController : Controller
    {
        public IActionResult Index()
        {
            var servicoAtor = new sakila.repositorio.servico.ServicoActor();
            var atores = servicoAtor.Listar();
            return View(atores);
        }
    }
}