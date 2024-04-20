using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
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
        public async Task<IActionResult> AddRegion([FromBody] RegionPostDto city)
        {
            try
            {
                Region region = new Region();
                region.Name = city.Name;
                region.CreatedOn = DateTime.Now;
                region.CreatedBy = null;

                _context.Regions.Add(region);
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
                return BadRequest();
            }

            try
            {
                var regionToUpdate = await _context.Regions.FindAsync(id);

                if (regionToUpdate == null)
                {
                    return NotFound();
                }

                regionToUpdate.Name = region.Name;
                regionToUpdate.UpdatedOn = DateTime.Now;
                regionToUpdate.UpdatedBy = null;

                _context.Entry(regionToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
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
            try
            {
                var regionToDelete = await _context.Regions.FindAsync(id);

                if (regionToDelete == null)
                {
                    return NotFound();
                }

                _context.Regions.Remove(regionToDelete);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}