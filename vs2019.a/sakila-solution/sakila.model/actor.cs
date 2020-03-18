using System;
using System.Collections.Generic;

namespace sakila.model
{
    public class actor
    {
        public int actor_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime last_update { get; set; }
        public virtual ICollection<film_actor> films_actors { get; set; }
    }
}
