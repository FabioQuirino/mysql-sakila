using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sakila.model;

namespace sakila.repositorio.servico
{
    public class servicoActor
    {
        public List<actor> Listar()
        {
            List<actor> atores = null;

            using (var db = new SakilaContext())
            {
                atores = db.actors.ToList();
            }

            return atores;
        }
    }
}
