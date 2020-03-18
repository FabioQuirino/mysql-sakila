using System;

namespace sakila.model
{
    public class film_actor
    {
        public int actor_id { get; set; }
        public virtual actor actor { get; set; }
        public int film_id { get; set; }
        public virtual film film { get; set; }
        public DateTime last_update { get; set; }
    }
}
