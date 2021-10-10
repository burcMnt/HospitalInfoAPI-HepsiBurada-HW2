using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Dtos.Hospital
{
    public class HospitalDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string[] Clinics { get; set; }

    }
}
