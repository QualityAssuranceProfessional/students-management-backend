using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class CitiesController : Controller
    {
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public CitiesController(SchoolManagementSystemContext context)
        {
            _context = context;
        }


        // GET: api/Cities

        [HttpGet]
        public async Task<IActionResult> GetCities(int page = 1, int pageSize = 10)
        {
            try
            {
                var cities = await _context.Cities
                    .Select(c=> new CityDto 
                    {
                        CityId = c.CityId,
                        Name = c.Name,
                        CreatedOn = c.CreatedOn
                    })
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(cities);

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Post api/Cities
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CityPostDto city)
        {
            try
            {
                City cityobj = new City();
                cityobj.Name = city.Name;
                cityobj.CreatedOn = DateTime.Now;
                cityobj.CreatedBy = null;

                _context.Cities.Add(cityobj);
                await _context.SaveChangesAsync();
                return Ok(cityobj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] CityDto city)
        {
                City cityobj = new City();
                cityobj.CityId = city.CityId;
                cityobj.Name = city.Name;
                cityobj.CreatedOn = DateTime.Now;
                cityobj.CreatedBy = null;

                if (cityobj ==null || cityobj.CityId ==0)
                {
                    if (cityobj != null)
                    {
                        return BadRequest("City data is invalid");
                    }
                    else if(cityobj.CityId == 0)
                    {
                        return BadRequest($"City Id{cityobj.CityId} is invalid");
                    }
                }


            try
            {
                var cities = await _context.Cities.FindAsync(cityobj.CityId);
                if(cities == null)
                {
                    return NotFound($"City Id{cityobj.CityId} is invalid");
                }
                cities.Name = city.Name;
                cities.CreatedOn = DateTime.Now;
                cities.CreatedBy = null;
                _context.SaveChanges();
                return Ok("City detailes Updated");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteCity(CityDeleteDto City)
        {
            try

             {
                var cityobj = _context.Cities.Find(City.CityId);
                if (cityobj == null)
                {
                    return NotFound($"City not Found with id {City.CityId}");
                }
                _context.Cities.Remove(cityobj);
                _context.SaveChanges();
                return Ok("City detailes Deleted");

            }
	         catch (Exception ex)

            {
                return BadRequest(ex.Message);

            }
        }


        // update api/Cities


        // delete api/Cities

    }
}
