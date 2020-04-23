using System;
using System.Collections.Generic;
using System.Text;

namespace sakila.model
{
    public class film
    {
        public int film_id { get; set; }
        public string title { get; set; }
        public int language_id { get; set; }
        public string description { get; set; }
        public short? release_year { get; set; }
        public short? original_language_id { get; set; }
        public short rental_duration { get; set; }
        public decimal rental_rate { get; set; }
        public short? length { get; set; }
        public decimal replacement_cost { get; set; }
        public string rating { get; set; }
        public string special_features { get; set; }
        public DateTime last_update { get; set; }
        public virtual ICollection<film_actor> films_actors { get; set; }
    }
}
