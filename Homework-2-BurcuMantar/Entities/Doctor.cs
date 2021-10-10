using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_2_BurcuMantar.Entities
{
    public class Doctor : BaseEntity
    {
        [Required(ErrorMessage = "This area is required"), MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("(M|F|N)", ErrorMessage = "You can enter only one character that is  ' M ' (Male) or ' F '(Female) or ' N ' (Not defined).")]
        public string Gender { get; set; }

        public string Clinic { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }

        public virtual ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}
