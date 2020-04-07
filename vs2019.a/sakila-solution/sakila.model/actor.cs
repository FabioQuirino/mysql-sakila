using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sakila.model
{
    public class actor
    {
        [Required(ErrorMessage = "Ator sem a identificação!")]
        public int actor_id { get; set; }

        [Required(ErrorMessage = "Digite um Nome!")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Digite um Sobrenome!")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Forneça uma Data!")]
        /* [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)] */
        public DateTime last_update { get; set; }
        public virtual ICollection<film_actor> films_actors { get; set; }
    }
}
