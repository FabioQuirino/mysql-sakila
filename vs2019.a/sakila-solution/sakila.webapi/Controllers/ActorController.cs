using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sakila.model;
using sakila.repositorio.servico;

namespace sakila.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<actor> Get()
        {
            var atores = new ServicoActor().Listar();
            return atores;
        }

    }
}
