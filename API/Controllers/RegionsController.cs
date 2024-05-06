using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class RegionsController : Controller
    {
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public RegionsController(SchoolManagementSystemContext context)
        {
            _context = context;
        }


        // GET: api/Regions

        [HttpGet]
        public async Task<IActionResult> GetRegions(int page = 1, int pageSize = 10)
        {
            try
            {
                var regions = await _context.Regions
                      .Where(r => r.Status == 1)
                      .Select(r => new RegionDto
                      {
                          RegionId = r.RegionId,
                          Name = r.Name,
                          CreatedOn = r.CreatedOn
                      })
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();

                return Ok(regions);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Post api/Regions
        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] RegionPostDto region)
        {
            try
            {
                Region regionobj = new Region();
                regionobj.Name = region.Name;
                regionobj.CreatedOn = DateTime.Now;
                regionobj.CreatedBy = null;
                regionobj.Status = 1;
                regionobj.CityId = region.CityId;

                _context.Regions.Add(regionobj);
                await _context.SaveChangesAsync();

                return Ok(region);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // update api/Regions
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(int id, [FromBody] RegionPutDto region)
        {
            if (id != region.RegionId)
            {
                return BadRequest("The region was not found.");
            }

            try
            {
                var regionToUpdate = await _context.Regions.FindAsync(id);

                if (regionToUpdate == null)
                {
                    return BadRequest("The region was not found.");
                }

                regionToUpdate.Name = region.Name;
                regionToUpdate.UpdatedOn = DateTime.Now;
                regionToUpdate.UpdatedBy = null;
                regionToUpdate.Status = 1;
                regionToUpdate.CityId = region.CityId;

                _context.Entry(regionToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("The region was updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Regions
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var regionToDelete = await _context.Regions.FindAsync(id);

            if (regionToDelete == null)
            {
                return NotFound("The region was not found.");
            }
            try
            {
                regionToDelete.Status = 9;
                regionToDelete.UpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok("The region was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}