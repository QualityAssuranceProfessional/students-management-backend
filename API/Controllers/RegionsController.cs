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



        [HttpGet]
        public async Task<IActionResult> GetRegions(int page = 1, int pageSize = 10)
        {
            try
            {
                var regions = await _context.Regions
                    .Select(r => new RegionDto
                    {
                        RegionId = r.RegionId,
                        Name=r.Name,
                        CityId = (int)r.RegionId,
                        CreatedOn = DateTime.Now,

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
        public async Task<IActionResult> AddRegion([FromBody] RegionPostDto Region)
        {
            try
            {
                Region Regionobj = new Region();
                Regionobj.Name = Region.Name;
                Regionobj.CityId = Region.CityId;
                Regionobj.CreatedOn = DateTime.Now;
                

                _context.Regions.Add(Regionobj);
                await _context.SaveChangesAsync();
                return Ok(Regionobj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //put api/Regions
        [HttpPut]
        public async Task<IActionResult> UpdateRegion([FromBody] RegionDto region)
        {
            Region regionobj = new Region();
            regionobj.RegionId = region.RegionId;
            regionobj.CityId = region.CityId;
            regionobj.Name = region.Name;
            regionobj.CreatedOn = DateTime.Now;
            

            if (regionobj == null || regionobj.CityId == 0)
            {
                if (regionobj != null)
                {
                    return BadRequest("Region data is invalid");
                }
                else if (regionobj.RegionId == 0)
                {
                    return BadRequest($"Region Id{regionobj.RegionId} is invalid");
                }
            }


            try
            {
                var regions = await _context.Regions.FindAsync(regionobj.RegionId);
                if (regions == null)
                {
                    return NotFound($"Region Id{regionobj.RegionId} is invalid");
                }
                regions.RegionId = regionobj.RegionId;
                regions.Name = region.Name;
                regions.CreatedOn = DateTime.Now;
                regions.CreatedBy = null;
                _context.SaveChanges();
                return Ok("Region detailes Updated");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteRegion(RegionDeleteDto Region)
        {
            try

            {
                var Regionobj = _context.Regions.Find(Region.RegionId);
                if (Regionobj == null)
                {
                    return NotFound($"Region not Found with id {Region.RegionId}");
                }
                _context.Regions.Remove(Regionobj);
                _context.SaveChanges();
                return Ok("Regio detailes Deleted");

            }
            catch (Exception ex)

            {
                return BadRequest(ex.Message);

            }
        }


    }
}
