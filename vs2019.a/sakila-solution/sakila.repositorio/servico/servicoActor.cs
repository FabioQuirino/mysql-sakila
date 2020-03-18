﻿using System.Collections.Generic;
using System.Linq;
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
                atores = db.actors.ToList();
            }

            return atores;
        }
    }
}
