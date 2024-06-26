﻿using API.DTOs;
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
                   .Where(r => r.Status == 1)
                   .Select(c => new CityDto
                   {
                       CityId = c.CityId,
                       Name = c.Name,
                       CreatedOn = c.CreatedOn
                   })
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();

                return Ok(cities);

            }
            catch (Exception ex)
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
                cityobj.Status = 1;

                _context.Cities.Add(cityobj);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update api/Cities
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityPutDto city)
        {
            if (id != city.CityId)
            {
                return BadRequest("The city was not found.");
            }

            if (String.IsNullOrEmpty(city.Name))
            {
                return StatusCode(404, "the city is empty !!");
            }

            try
            {
                var cityToUpdate = await _context.Cities.Where(x=>x.CityId == city.CityId).SingleOrDefaultAsync();

                if (cityToUpdate == null)
                {
                    return BadRequest("The city was not found.");
                }

                cityToUpdate.Name = city.Name;
                cityToUpdate.UpdatedOn = DateTime.Now;
                cityToUpdate.UpdatedBy = null;
                cityToUpdate.Status = 1;

                await _context.SaveChangesAsync();

                return Ok("The city was updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // delete api/Cities
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var cityToDelete = await _context.Cities.FindAsync(id);

            if (cityToDelete == null)
            {
                return NotFound("The city was not found.");
            }

            try
            {
                cityToDelete.Status = 9;
                cityToDelete.UpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok("The city was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}