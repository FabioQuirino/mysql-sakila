using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using sakila.model;

namespace sakila.repositorio.servico
{
    public class ServicoActor
    {
        public List<actor> Listar()
        {
            List<actor> atores = null;

            using (var db = new SakilaContext())
            {
                atores = db.actors
                    .Include(x => x.films_actors)
                    .ThenInclude(y => y.film)
                    .ToList();
            }

            return atores;
        }

        public actor ObterPorSobrenome(string sobrenome)
        {
            actor ator = null;

            using (var db = new SakilaContext())
            {
                ator = db.actors.FirstOrDefault(x => x.last_name.Contains(sobrenome));
            }

            return ator;
        }

        public actor GetById(int? primarykey)
        {
            actor ator = null;

            using (var db = new SakilaContext())
            {
                ator = db.actors.Find(primarykey);
            }

            return ator;
        }

        public void Insert(actor ator)
        {
            using (var db = new SakilaContext())
            {
                db.actors.Add(ator);
                db.SaveChanges();
            }
        }
        public void Update(actor ator)
        {
            using (var db = new SakilaContext())
            {
                db.actors.Update(ator);
                db.SaveChanges();
            }
        }

        public void Delete(actor ator)
        {
            using(var db = new SakilaContext())
            {
                db.actors.Remove(ator);
                //db.film_actors.Remove(ator);
                db.SaveChanges();
            }
        }
        
    }
}
