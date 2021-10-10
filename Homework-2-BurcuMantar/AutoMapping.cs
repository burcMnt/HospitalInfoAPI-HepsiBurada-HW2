using AutoMapper;
using Homework_2_BurcuMantar.Dtos;
using Homework_2_BurcuMantar.Dtos.Doctors;
using Homework_2_BurcuMantar.Dtos.Hospital;
using Homework_2_BurcuMantar.Dtos.Patient;
using Homework_2_BurcuMantar.Entities;

namespace Homework_2_BurcuMantar
{
    public class AutoMapping :Profile
    {
        public AutoMapping()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();
            CreateMap<Doctor, DoctorUpdateDto>();
            CreateMap<DoctorUpdateDto, Doctor>();
            CreateMap<Doctor, DoctorFieldUpdateDto>();
            CreateMap<DoctorFieldUpdateDto, Doctor>();
            CreateMap<Hospital, HospitalDto>();
            CreateMap<HospitalDto, Hospital>();
            CreateMap<Hospital, HospitalUpdateDto>();
            CreateMap<HospitalUpdateDto, Hospital>();
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<Patient, PatientUpdateDto>();
            CreateMap<PatientUpdateDto, Patient>();
            CreateMap<DoctorPatient, DoctorPatientDto>();
            CreateMap<DoctorPatientDto, DoctorPatient>();
        }
    }
}
