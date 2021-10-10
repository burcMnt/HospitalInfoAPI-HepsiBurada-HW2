using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homework_2_BurcuMantar.Entities
{
    public class Hospital : BaseEntity
    {
        [Required(ErrorMessage = "This area is required")]
        public string Address { get; set; }
        public string[] Clinics { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
       
    }
}
