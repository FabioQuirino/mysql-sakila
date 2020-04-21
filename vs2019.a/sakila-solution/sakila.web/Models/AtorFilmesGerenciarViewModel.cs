using sakila.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sakila.web.Models
{
    public class AtorFilmesGerenciarViewModel
    {
        public actor actor { get; set; }

        public List<film> filmes { get; set; }
    }
}
