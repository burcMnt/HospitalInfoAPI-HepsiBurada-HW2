using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_2_BurcuMantar.Entities
{
    public class Patient :BaseEntity
    {
        [Required(ErrorMessage = "This area is required"), MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("(M|F|N)", ErrorMessage = "You can enter only one character that is  ' M ' (Male) or ' F '(Female) or ' N ' (Not defined).")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("[0-9](10)", ErrorMessage = "This area is required and only accept maximum ten number between 0-9 ")]
        public long FileNumber { get; set; }

        public virtual ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}
