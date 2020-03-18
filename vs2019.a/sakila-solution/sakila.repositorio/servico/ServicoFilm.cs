using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                films = db.films.ToList();
            }

            return films;
        }
    }
}
