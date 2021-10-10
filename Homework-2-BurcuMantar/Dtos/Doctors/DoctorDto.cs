using Homework_2_BurcuMantar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Dtos.Doctors
{
    public class DoctorDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("(M|F|N)", ErrorMessage = "You can enter only one character that is  ' M ' (Male) or ' F '(Female) or ' N ' (Not defined).")]
        public string Gender { get; set; }
        public string Clinic { get; set; }
        public int HospitalId { get; set; }

    }
}
