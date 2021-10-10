using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Dtos.Doctors
{
    public class DoctorUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Clinic { get; set; }
        public int HospitalId { get; set; }
    }
}
