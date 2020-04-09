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

        public actor GetActorFilmById(int id)
        {
            actor ator = null;

            using (var db = new SakilaContext())
            {
                ator = db.actors
                .Include(x => x.films_actors)
                .ThenInclude(y => y.film)
                .Where(a=>a.actor_id==id)
                .First();
            }

            return ator;
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
                ator = ObterPorId(db, primarykey.Value);
            }

            return ator;
        }

        private actor ObterPorId(SakilaContext db, int primaryKey)
        {
            return db.actors.Find(primaryKey);
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
            using (var db = new SakilaContext())
            //Como não há concorrência nas modificações, não é necessário criar um contexto transacional.
            //using (var transacao = db.Database.BeginTransaction())
            {
                try
                {
                    db.films_actors.RemoveRange(ator.films_actors);
                    db.actors.Remove(ator);

                    db.SaveChanges();
                    //transacao.Commit();
                }
                catch
                {
                    //transacao.Rollback();
                    //tratar exceção e gravar no log além de fechar a transação
                    throw;
                }
/*                 finally
                {
                    transacao.Dispose();
                };
 */            }
        }
    }
}
