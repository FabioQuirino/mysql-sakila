using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using sakila.model;

namespace sakila.repositorio.servico
{
    public class ServicoFilm
    {
        public List<film> Listar()
        {
            List<film> films = null;

            using (var db = new SakilaContext())
            {
                films = db.films
                    .Include(x => x.films_actors)
                    .ThenInclude(y => y.actor)
                    .ToList();
            }

            return films;
        }

        public void Incluir(film filme)
        {
            using (var db = new SakilaContext())
            {
                db.films.Add(filme);
                db.SaveChanges();
            }
        }
    }
}
