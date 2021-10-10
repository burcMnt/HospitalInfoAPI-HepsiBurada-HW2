using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This area is required")]
        public string Name { get; set; }
    }
}
