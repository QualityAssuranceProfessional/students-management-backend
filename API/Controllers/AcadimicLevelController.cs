using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class AcadimicLevelController : Controller
    {
        private readonly SchoolManagementSystemContext _context;
        public AcadimicLevelController(SchoolManagementSystemContext context)
        {
            _context = context;
        }

        // Get api Academic Level
        [HttpGet]
        public async Task<IActionResult> GetAcadimicLevels(int page = 1, int pageSize = 10)
        {
            try
            {
                var academicLevels = await _context.AcadimicLevels
                   .Where(m => m.Status == 1)
                   .Select(m => new AcademicLevelDto
                   {
                       AcadimicLevelId = m.AcadimicLevelId,
                       Name = m.Name,
                       CreatedOn = m.CreatedOn
                   })
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();

                return Ok(academicLevels);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Post api/Academic Year Level
        [HttpPost]
        public async Task<IActionResult> AddAcademicLevel([FromBody] AcademicLevelPostDto academicL)
        {
  
            
            try
            {
                if (academicL.AcadmicLevelType == 1 || academicL.AcadmicLevelType == 2 || academicL.AcadmicLevelType == 3)
                {
                    AcadimicLevel academicLobj = new AcadimicLevel();
                 
                    
                    academicLobj.Name = academicL.Name;
                    academicLobj.CreatedOn = DateTime.Now;
                    academicLobj.CreatedBy = null;
                    academicLobj.Status = 1;
                    academicLobj.AcadmicLevelType = academicL.AcadmicLevelType;
                   
                    _context.AcadimicLevels.Add(academicLobj);
                    await _context.SaveChangesAsync();
                    return Ok(academicLobj);
                }

                return BadRequest("Error entering Academic AcadmicLevelType");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update api/AcademicLevels
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcademicLevel(short id, [FromBody] AcadimicLPutDto academicLP)
        {

            try
            {

                if (id != academicLP.AcadimicLevelId)
                {
                    return BadRequest("The Academic Year was not found.");
                }

                if (String.IsNullOrEmpty(academicLP.Name))
                {
                    return StatusCode(404, "the Academic Year is empty !!");
                }
                var academicLUpdate = await _context.AcadimicLevels.Where
                    (a => a.AcadimicLevelId == academicLP.AcadimicLevelId).SingleOrDefaultAsync();
                if (academicLUpdate == null)
                {
                    return BadRequest("The academic was not found.");
                }
                academicLUpdate.Name = academicLP.Name;
                academicLUpdate.UpdatedOn = DateTime.Now;
                academicLUpdate.UpdatedBy = null;
                academicLUpdate.Status = 1;
              
                academicLUpdate.AcadmicLevelType = academicLP.AcadmicLevelType;
                await _context.SaveChangesAsync();
                return Ok("The Academic Level was updated successfully.");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicLevel(short id)
        {
            try
            {

                var academicDelete = await _context.AcademicYears.FindAsync(id);

                if (academicDelete == null)
                {
                    return NotFound("The Academic Level was not found.");
                }

                academicDelete.UpdatedOn = DateTime.Now;
                academicDelete.Status = 9;
                await _context.SaveChangesAsync();
                return Ok("The academic Level was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
