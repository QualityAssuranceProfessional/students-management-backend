using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.Controllers
{

    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class AcademicYearController : Controller
    {
        private readonly SchoolManagementSystemContext _context;
        public AcademicYearController(SchoolManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAcademicYear(int page = 1, int pageSize = 10)
        {
            try
            {
                var academicyears = await _context.AcademicYears
                   .Where(r => r.Status == 1)
                   .Select(c => new AcademicYearDto
                   {
                       AcademicYearId = c.AcademicYearId,
                       AcademicYearName = c.AcademicYearName,
                       CreatedOn = c.CreatedOn
                   })
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();

                return Ok(academicyears);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddAcademicYear([FromBody]AcademicYearPostDto academicY)
        {
          
           
            try
            {
                AcademicYear academicYobj = new AcademicYear();
                //var ac = await _context.AcademicYears.Where(r => r.AcademicYearName == academicY.AcademicYearName).SingleAsync();
                //if(ac != null)
                //{
                //    return BadRequest("The AcademicYear was not found");
                //}
                //  var ac = _context.AcademicYears.Where(r => r.AcademicYearName == academicY.AcademicYearName).SingleOrDefaultAsync();


                academicYobj.AcademicYearName = academicY.AcademicYearName;
                academicYobj.CreatedOn = DateTime.Now;
                academicYobj.CreatedBy = null;
                academicYobj.Status = 1;
              
                _context.AcademicYears.Add(academicYobj);
                await _context.SaveChangesAsync();
                return Ok(academicYobj);
              

            }
           
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update api/AcademicYears
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcademicYear(short id, [FromBody] AcademicYPutDto academic)
        {
            if (id != academic.AcademicYearId)
            {
                return BadRequest("The Academic Year was not found.");
            }

            if (String.IsNullOrEmpty(academic.AcademicYearName))
            {
                return StatusCode(404, "the Academic Year is empty !!");
            }

            try
            {
                var academicUpdate = await _context.AcademicYears.Where
                    (a => a.AcademicYearId == academic.AcademicYearId).SingleOrDefaultAsync();
                if (academicUpdate == null)
                {
                    return BadRequest("The academic was not found.");
                }
                academicUpdate.AcademicYearName = academic.AcademicYearName;
                academicUpdate.UpdatedOn = DateTime.Now;
                academicUpdate.UpdatedBy = null;
                academicUpdate.Status = 1;
                await _context.SaveChangesAsync();
                return Ok("The Academic Year was updated successfully.");

            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicYear(short id)
        {
            var academicDelete = await _context.AcademicYears.FindAsync(id);

            if (academicDelete == null)
            {
                return NotFound("The Academic Year was not found.");
            }
            try
            {
               
                academicDelete.UpdatedOn = DateTime.Now;
                academicDelete.Status = 9;
                await _context.SaveChangesAsync();
                return Ok("The academic year was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
