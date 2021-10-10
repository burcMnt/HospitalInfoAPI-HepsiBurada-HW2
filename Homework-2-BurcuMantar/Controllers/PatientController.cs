using AutoMapper;
using Homework_2_BurcuMantar.Dtos.Patient;
using Homework_2_BurcuMantar.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Controllers
{
    [Route("api/v2/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly ProjectDbContext _db;
        private readonly IMapper _mapper;

        public PatientController(ILogger<PatientController> logger, ProjectDbContext db, IMapper mapper)
        {
           _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        //You can use the HttpGet request to take all patientlist, and also you can take patient list with using specific filternames as name,lastname etc.
        //https://localhost:44392/api/v2/patients or https://localhost:44392/api/v2/patients?filter=gender
        [HttpGet]
        public IActionResult Get(string filter)
        {
            _logger.LogInformation("User requested the PatientController's Get method.");

            if (string.IsNullOrEmpty(filter))
            {
                return Ok(_db.Patients.ToList());
            }
            else
            {
                string keyword = filter.ToLower();
                List<string> filteredList = new List<string>();
                switch (keyword)
                {
                    case "name":
                        foreach (var pt in _db.Patients.ToList())
                        {
                            filteredList.Add(pt.Name.ToString());
                        }
                        return Ok(filteredList);

                    case "lastname":
                        foreach (var pt in _db.Patients.ToList())
                        {
                            filteredList.Add(pt.LastName.ToString());
                        }
                        return Ok(filteredList);

                    case "gender":
                        foreach (var pt in _db.Patients.ToList())
                        {
                            filteredList.Add(pt.Gender.ToString());
                        }
                        return Ok(filteredList);
                    case "filenumber":
                        foreach (var pt in _db.Patients.ToList())
                        {
                            filteredList.Add(pt.FileNumber.ToString());
                        }
                        return Ok(filteredList);
                    case "patientsdr":
                        foreach (var pt in _db.DoctorPatients.ToList())
                        {
                            List<Doctor> drList = _db.Doctors.Where(x => x.Id == pt.DoctorId).ToList();
                            if (drList != null)
                            {
                                foreach (var item in drList)
                                {
                                    filteredList.Add(item.Name);
                                }
                            }

                        }
                        return Ok(filteredList);
                    default:
                        return Ok(_db.Patients.ToList());
                }
            }

        }

        //You can use this HttpGet request with id  to get just one patient object.
        //https://localhost:44392/api/v2/patients/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPt(int id)
        {
            _logger.LogInformation("User requested the PatientController's GetPt by id method.");

            Patient patient = _db.Patients.FirstOrDefault(x => x.Id.Equals(id));
            var model = _mapper.Map<PatientDto>(patient);
            if (model == null)
            {
                return NotFound();
            }
            return await Task.FromResult(Ok(model));
        }

        //You can use this HttpPost request to create new patient's object.
        //https://localhost:44392/api/v2/patients
        [HttpPost]
        public IActionResult CreatePt([FromBody] PatientDto pt)
        {
            _logger.LogInformation("User requested the PatientController's CreatePt method.");

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var patient = _mapper.Map<Patient>(pt);
            _db.Patients.Add(patient);
            _db.SaveChanges();
            return CreatedAtAction("GetPt", new { Id = patient.Id }, pt);
        }

        //You can use this HttpPut request to update patient's object already have with using unique id.
        //https://localhost:44392/api/v2/patients/5
        [HttpPut("{id}")]
        public IActionResult UpdatePt([FromRoute] int id, [FromBody] PatientUpdateDto pt)
        {
            _logger.LogInformation("User requested the PatientController's UpdatePt method.");

            if (ModelState.IsValid)
            {
                var patient = _mapper.Map<Patient>(pt);
                if (id != patient.Id)
                {
                    return BadRequest("Id information is not confirmed");
                }
                if (!_db.Patients.Any(x => x.Id.Equals(id)))
                {
                    return NotFound();
                }
                _db.Patients.Update(patient);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);

        }

        //You can use this HttpDelete request to delete patient's object already have with using unique id.
        //https://localhost:44392/api/v2/patients/4
        [HttpDelete("{id}")]
        public IActionResult DeletePt(int id)
        {
            _logger.LogInformation("User requested the PatientController's DeletePt method.");

            var pt = _db.Patients.FirstOrDefault(x => x.Id.Equals(id));
            if (pt == null)
            {
                return NotFound();
            }
            _db.Patients.Remove(pt);
            _db.SaveChanges();
            return NoContent();
        }




    }
}
