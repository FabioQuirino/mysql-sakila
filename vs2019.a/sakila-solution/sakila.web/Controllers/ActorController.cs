using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sakila.model;

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

        public ViewResult DetailsActor(int id)
        {
            var servicoAtor = new sakila.repositorio.servico.ServicoActor();

            actor ator = servicoAtor.GetActorFilmById(id);

            return View("Views/Actor/DetailsActor.cshtml", ator);
        }

        public ViewResult EditActor(int? id)
        {
            actor ator = GetById(id);

            return View("Views/Actor/EditActor.cshtml", ator);
        }

        public ViewResult UpdateActor(actor ator)
        {
            var servicoAtor = new sakila.repositorio.servico.ServicoActor();

            ator.last_update = DateTime.Now;

            servicoAtor.Update(ator);

            var atores = servicoAtor.Listar();

            TempData["mensagem"] = "Ator atualizado com Sucesso!";

            return View("Views/Actor/Index.cshtml",atores);
        }

        public ViewResult CreateActor()
        {
            return View("Views/Actor/CreateActor.cshtml");  
        }

        [ValidateAntiForgeryToken]
        public ViewResult InsertActor(actor ator)
        {
            var servicoAtor = new sakila.repositorio.servico.ServicoActor();
            
            ator.last_update = DateTime.Now;

            servicoAtor.Insert(ator);

            var atores = servicoAtor.Listar();

            TempData["mensagem"] = "Ator Criado com Sucesso!";

            return View("Views/Actor/Index.cshtml", atores);

        }

        public ViewResult ConfirmDeleteActor(int id)
        {
            var servicoAtor = new sakila.repositorio.servico.ServicoActor();

            actor ator = servicoAtor.GetActorFilmById(id);

            return View("Views/Actor/ConfirmDeleteActor.cshtml", ator);
        }

        // Apagar o registro da tabela actor e da tabela film_actor

        [ValidateAntiForgeryToken]
        public ViewResult DeleteActor(int id)
        {
            //actor ator = GetById(id);

            var servicoAtor = new sakila.repositorio.servico.ServicoActor();

            actor ator = servicoAtor.GetActorFilmById(id);

            servicoAtor.Delete(ator);

            var atores = servicoAtor.Listar();

            TempData["mensagem"] = "Ator apagado com Sucesso!";

            return View("Views/Actor/Index.cshtml", atores);
        }

        private actor GetById(int? id)
        {
            VerifyNullIntParameter(id);
            var servicoAtor = new sakila.repositorio.servico.ServicoActor();
            actor ator = servicoAtor.GetById(id);

            return ator;
        }

        private void VerifyNullIntParameter(int? id)
        {
            if (id is null)
            {
                ModelState.AddModelError("", "Erro ao Buscar o Ator! Ator não encontrado!");
                //throw new ArgumentNullException(nameof(id));
            }
        }


    }
}