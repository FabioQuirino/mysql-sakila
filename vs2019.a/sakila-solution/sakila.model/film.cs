using System;
using System.Collections.Generic;
using System.Text;

namespace sakila.model
{
    public class film
    {
        public int film_id { get; set; }
        public string title { get; set; }
        public virtual ICollection<film_actor> films_actors { get; set; }
    }
}
