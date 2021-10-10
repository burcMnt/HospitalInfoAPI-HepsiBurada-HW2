using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Dtos.Patient
{
    public class PatientUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public long FileNumber { get; set; }
    }
}
