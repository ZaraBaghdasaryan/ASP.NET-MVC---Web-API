using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace VOD.Common.Entities
{
   public class School
    {
        [Key]
        public int Id { get; set; }

        [MaxLength (50), Required]
        public string Name { get; set; }
    }
}
