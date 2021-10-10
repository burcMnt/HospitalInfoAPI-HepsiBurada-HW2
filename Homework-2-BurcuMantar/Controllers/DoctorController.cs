using AutoMapper;
using Homework_2_BurcuMantar.Dtos.Doctors;
using Homework_2_BurcuMantar.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Controllers
{
    [Route("api/v2/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly ProjectDbContext _db;
        private readonly IMapper _mapper;

        public DoctorController(ILogger<DoctorController> logger, ProjectDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        //You can use the HttpGet request to take all drlist, and also you can take dr list with using specific filternames as name,lastname,hospital etc.
        //https://localhost:44392/api/v2/doctors or https://localhost:44392/api/v2/doctors?filter=name
        [HttpGet]
        public IActionResult Get(string filter)
        {
            _logger.LogInformation("User requested the DoctorController's Get method.");

            try
            {
                if (string.IsNullOrEmpty(filter))
                {
                    foreach (var item in _db.Doctors.ToList())
                    {
                        _logger.LogInformation("The data : {Name} {LastName} {Gender} {Clinic}", item.Name,item.LastName,item.Gender,item.Clinic);
                    }
                    return Ok(_db.Doctors.ToList());
                }
                else
                {
                    string keyword = filter.ToLower();
                    List<string> filteredList = new List<string>();
                    switch (keyword)
                    {
                        case "name":
                            foreach (var dr in _db.Doctors.ToList())
                            {
                                _logger.LogInformation("The data : {Name}", dr.Name);
                                filteredList.Add(dr.Name.ToString());
                            }
                            return Ok(filteredList);

                        case "lastname":
                            foreach (var dr in _db.Doctors.ToList())
                            {
                                _logger.LogInformation("The data : {LastName}", dr.LastName);
                                filteredList.Add(dr.LastName.ToString());
                            }
                            return Ok(filteredList);

                        case "gender":
                            foreach (var dr in _db.Doctors.ToList())
                            {
                                _logger.LogInformation("The data : {Gender}", dr.Gender);
                                filteredList.Add(dr.Gender.ToString());
                            }
                            return Ok(filteredList);
                        case "clinic":
                            foreach (var dr in _db.Doctors.ToList())
                            {
                                _logger.LogInformation("The data : {Clinic}", dr.Clinic);
                                filteredList.Add(dr.Clinic.ToString());
                            }
                            return Ok(filteredList);
                        case "hospitalname":
                            foreach (var dr in _db.Doctors.ToList())
                            {
                                List<Hospital> hList = _db.Hospitals.Where(x => x.Id == dr.HospitalId).ToList();
                                if (hList != null)
                                {
                                    foreach (var item in hList)
                                    {
                                        _logger.LogInformation("The data : {HostName}", item.Name);
                                        filteredList.Add(item.Name);
                                    }
                                }

                            }
                            return Ok(filteredList);
                        default:
                            _logger.LogInformation("Filter paramater is not valid !");
                            return BadRequest("filter paramater is not valid !");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "We caught this acception in he DoctorContollors Get call.");
            }

            return Ok();

        }

        //You can use this HttpGet request to make specific search , for example to get the dr objects who name is Naz (?name=naz )
        //This api users should use only one parameter in the one query. Otherwise user gets badrequest error.
        //https://localhost:44392/api/v2/doctors/list?name=naz OR https://localhost:44392/api/v2/doctors/list?clinic=göz
        [Route("list")]
        [HttpGet]
        public IActionResult GetListedDr([FromQuery] string name, [FromQuery] string lastname, [FromQuery] string gender, [FromQuery] string clinic)
        {
            _logger.LogInformation("User requested the DoctorController's GetListedDr method.");

            List<Doctor> filteredDrList = new List<Doctor>();
            List<string> parameters = new List<string>() { name, lastname, gender, clinic };
            int count = 0;
            foreach (var item in parameters)
            {
                if (item != null)
                    count++;
            }

            if (count > 1)
            {
                return BadRequest("Can not enter more than 1 paramater value.");
            }

            if (!string.IsNullOrEmpty(name))
            {
                foreach (var dr in _db.Doctors.ToList())
                {
                    if ((dr.Name.ToLower().Contains(name.ToLower())))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(lastname))
            {
                foreach (var dr in _db.Doctors.ToList())
                {
                    if (dr.LastName.ToLower().Contains(lastname.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(gender))
            {
                foreach (var dr in _db.Doctors.ToList())
                {
                    if (dr.Gender.ToLower().Contains(gender.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(clinic))
            {
                foreach (var dr in _db.Doctors.ToList())
                {
                    if (dr.Clinic.ToLower().Contains(clinic.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }

            else
            {
                return BadRequest("There is not any parameter value to list. ");
            }
        }
        //You can use this HttpGet request with id  to get just one dr object.
        //https://localhost:44392/api/v2/doctors/4
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDr(int id)
        {
            _logger.LogInformation("User requested the DoctorController's GetDr by id method.");
            Doctor doctor = _db.Doctors.FirstOrDefault(x => x.Id.Equals(id));
            var model = _mapper.Map<DoctorDto>(doctor);
            if (model == null)
            {
                return NotFound();
            }
            return await Task.FromResult(Ok(model));
        }

        //You can use this HttpPost request to create new dr's object.
        //https://localhost:44392/api/v2/doctors
        [HttpPost]
        public IActionResult CreateDr([FromBody] DoctorDto dr)
        {
            _logger.LogInformation("User requested the DoctorController's CreateDr method.");

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var doctor = _mapper.Map<Doctor>(dr);
            _db.Doctors.Add(doctor);
            _db.SaveChanges();
            return CreatedAtAction("GetDr", new { Id = doctor.Id }, dr);
        }

        //You can use this HttpPut request to update dr's object already have with using unique id.
        //https://localhost:44392/api/v2/doctors/8
        [HttpPut("{id}")]
        public IActionResult UpdateDr([FromRoute] int id, [FromBody] DoctorUpdateDto dr)
        {
            _logger.LogInformation("User requested the DoctorController's UpdateDr method.");

            var doctor = _mapper.Map<Doctor>(dr);
            if (ModelState.IsValid)
            {
                if (id != doctor.Id)
                {
                    return BadRequest("Id information is not confirmed");
                }
                if (!_db.Doctors.Any(x => x.Id.Equals(id)))
                {
                    return NotFound();
                }
                _db.Doctors.Update(doctor);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);

        }

        //You can use this HttpDelete request to delete dr's object already have with using unique id.
        //https://localhost:44392/api/v2/doctors/8
        [HttpDelete("{id}")]
        public IActionResult DeleteDr(int id)
        {
            _logger.LogInformation("User requested the DoctorController's DeleteDr method.");

            var dr = _db.Doctors.FirstOrDefault(x => x.Id.Equals(id));
            if (dr == null)
            {
                return NotFound();
            }
            _db.Doctors.Remove(dr);
            _db.SaveChanges();
            return NoContent();
        }

        //You can use this HttpPacth request to update one field from dr object's  fields already have with using unique id and UpdateHospitalNameDto.cs class.
        //https://localhost:44392/api/v2/doctors/8
        [HttpPatch("{id}")]
        public IActionResult UpdateHospital([FromRoute] int id, [FromBody] DoctorFieldUpdateDto drclinic)
        {
            _logger.LogInformation("User requested the DoctorController's UpdateHospital method.");
            var doctor = _mapper.Map<Doctor>(drclinic);
            if (ModelState.IsValid)
            {
                var dr = _db.Doctors.FirstOrDefault(x => x.Id.Equals(id));
                if (dr == null)
                {
                    return NotFound();
                }
                foreach (var drc in _db.Doctors.ToList())
                {
                    if (id.Equals(drc.Id))
                    {
                        drc.Clinic = drclinic.Clinic;
                        _db.SaveChanges();
                        return Ok();
                    }
                }
            }
            return BadRequest(ModelState);

        }
    }
}

