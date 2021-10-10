using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Dtos.Doctors
{
    public class DoctorFieldUpdateDto
    {
        [Required]
        public string Clinic { get; set; }
    }
}
