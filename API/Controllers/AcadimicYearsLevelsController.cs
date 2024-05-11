using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class acadimicYearsLevelsController : Controller
    {
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public acadimicYearsLevelsController(SchoolManagementSystemContext context)
        {
            _context = context;
        }


        [HttpGet("academicYearLevels")]
        public ActionResult<IEnumerable<AcademicYearLevelDto>> GetAcademicYearLevels()
        {
            try
            {
                var acadimicYearsLevels = _context.AcadimicYearsLevels
                    .Include(a => a.AcademicYear)
                    .Include(a => a.AcadimicLevel)
                    .Select(a => new AcademicYearLevelDto
                    {
                        AcademicYearId = (short)a.AcademicYearId,
                        AcademicYearName = a.AcademicYear.AcademicYearName,
                        AcadimicLevelId = (short)a.AcadimicLevelId,
                        Name = a.AcadimicLevel.Name,
                      
                        AcadmicLevelType = a.AcadimicLevel.AcadmicLevelType

                    })
                    .ToList();
                return Ok(acadimicYearsLevels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }



        [HttpPost("academicYearLevel")]
        public async Task<ActionResult> CreateAcademicYearsLevel(short academicYearId, short acadimicLevelId)
        {
            try
            {
                var academicYear = await _context.AcademicYears.FindAsync(academicYearId);
                var academicLevel = await _context.AcadimicLevels.FindAsync(acadimicLevelId);

                if (academicYear == null  || academicLevel == null || academicYear.Status == 9 || academicLevel.Status == 9)
                {
                    return NotFound("Academic year or academic level not found");
                }
                var acadimicYearsLevel = new AcadimicYearsLevel
                {
                    AcademicYearId = academicYearId,
                    AcadimicLevelId = acadimicLevelId
                };

                _context.AcadimicYearsLevels.Add(acadimicYearsLevel);
                await _context.SaveChangesAsync();

                return Ok("Academic year level created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("acadimicYearsLevels")]
        public async Task<ActionResult> DeleteAcademicYearsLevel(short academicYearId, short acadimicLevelId)
        {
            try
            {
                var acadimicYearsLevels = _context.AcadimicYearsLevels
                    .FirstOrDefaultAsync(a => a.AcademicYearId == academicYearId && a.AcadimicLevelId == acadimicLevelId);
          

                if(acadimicYearsLevels == null)
                { 
                    return NotFound("Academic year level not found");
                  }

                _context.AcadimicYearsLevels.Remove(await acadimicYearsLevels.ConfigureAwait(false));
                await _context.SaveChangesAsync();
                return Ok("Academic year level deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        }
}
