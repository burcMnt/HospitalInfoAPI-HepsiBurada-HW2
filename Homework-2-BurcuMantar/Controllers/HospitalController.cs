using AutoMapper;
using Homework_2_BurcuMantar.Dtos.Hospital;
using Homework_2_BurcuMantar.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_2_BurcuMantar.Controllers
{
    [Route("api/v2/hospitals")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly ILogger<HospitalController> _logger;
        private readonly ProjectDbContext _db;
        private readonly IMapper _mapper;

        public HospitalController(ILogger<HospitalController> logger, ProjectDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        //You can use the HttpGet request to take all hospitalList, and also you can take hp list with using specific filternames as name,lastname,hospital etc.
        //https://localhost:44392/api/v2/hospitals or https://localhost:44392/api/v2/hospitals?filter=doctor

        [HttpGet]
        public IActionResult Get(string filter)
        {
            _logger.LogInformation("User requested the HospitalController's Get method.");

            if (string.IsNullOrEmpty(filter))
            {
                return Ok(_db.Hospitals.ToList());
            }
            else
            {
                string keyword = filter.ToLower();
                List<string> filteredList = new List<string>();
                switch (keyword)
                {
                    case "hospitalname":
                        foreach (var hp in _db.Hospitals.ToList())
                        {
                            filteredList.Add(hp.Name.ToString());
                        }
                        return Ok(filteredList);

                    case "address":
                        foreach (var hp in _db.Hospitals.ToList())
                        {
                            filteredList.Add(hp.Address.ToString());
                        }
                        return Ok(filteredList);

                    case "clinic":
                        foreach (var hp in _db.Hospitals.ToList())
                        {
                            
                            filteredList.Add(hp.Clinics.ToString());
                        }
                        return Ok(filteredList);
                    case "doctor":
                        foreach (var hp in _db.Hospitals.ToList())
                        {
                            List<Doctor> dList = _db.Doctors.Where(x => x.HospitalId == hp.Id).ToList();
                            if (dList != null)
                            {
                                foreach (var item in dList)
                                {
                                    filteredList.Add(item.Name+ " " + item.LastName);
                                }
                            }
                        }
                        return Ok(filteredList);
                    default:
                        return Ok(_db.Hospitals.ToList());
                }
            }
        }

        //You can use this HttpGet request with id  to get just one hospital object.
        //https://localhost:44392/api/v2/hospitals/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHp(int id)
        {
            _logger.LogInformation("User requested the HospitalController's GetHp by id method.");
            Hospital hospital = _db.Hospitals.FirstOrDefault(x => x.Id.Equals(id));
            var model = _mapper.Map<HospitalDto>(hospital);
            if (model == null)
            {
                return NotFound();
            }
            return await Task.FromResult(Ok(model));
        }

        //You can use this HttpPost request to create new hospital's object.
        //https://localhost:44392/api/v2/hospitals
        [HttpPost]
        public IActionResult CreateHp([FromBody] HospitalDto hp)
        {
            _logger.LogInformation("User requested the HospitalController's CreateHp  method.");

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var hospital = _mapper.Map<Hospital>(hp);
            _db.Hospitals.Add(hospital);
            _db.SaveChanges();
            return CreatedAtAction("GetHp", new { Id = hospital.Id }, hp);
        }

        //You can use this HttpPut request to update hospital's object already have with using unique id.
        //https://localhost:44392/api/v2/hospitals/4
        [HttpPut("{id}")]
        public IActionResult UpdateHp([FromRoute] int id, [FromBody] HospitalUpdateDto hp)
        {
            _logger.LogInformation("User requested the HospitalController's UpdateHp  method.");

            var hospital = _mapper.Map<Hospital>(hp);
            if (ModelState.IsValid)
            {
                if (id != hospital.Id)
                {
                    return BadRequest("Id information is not confirmed");
                }
                if (!_db.Hospitals.Any(x => x.Id.Equals(id)))
                {
                    return NotFound();
                }
                _db.Hospitals.Update(hospital);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);

        }

        //You can use this HttpDelete request to delete hospital's object already have with using unique id.
        //https://localhost:44392/api/v2/hospitals/4
        [HttpDelete("{id}")]
        public IActionResult DeleteHp(int id)
        {
            _logger.LogInformation("User requested the HospitalController's DeleteHp  method.");
            var hp = _db.Hospitals.FirstOrDefault(x => x.Id.Equals(id));
            if (hp == null)
            {
                return NotFound();
            }
            _db.Hospitals.Remove(hp);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
