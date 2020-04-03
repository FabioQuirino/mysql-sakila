using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sakila.model;
using sakila.repositorio.servico;

namespace sakila.web.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        public IActionResult Index()
        {
            var filmeServico = new ServicoFilm();
            var fillmes = filmeServico.Listar();

            return View(fillmes);
        }

        // GET: Film/Details/5
        public IActionResult Details(int id)
        {
            var filme = Obter(id);
            return View(filme);
        }

        // GET: Film/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Film/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Film/Edit/5
        public ActionResult Edit(int id)
        {
            var filme = Obter(id);
            return View(filme);
        }

        // POST: Film/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Film/Delete/5
        public ActionResult Delete(int id)
        {
            var filme = Obter(id);
            return View(filme);
        }

        // POST: Film/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private film Obter(int id)
        {
            var filmService = new ServicoFilm();
            film filme = filmService.Obter(id);
            return filme;
        }
    }
}