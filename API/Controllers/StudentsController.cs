using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class StudentsController : Controller
    {
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public StudentsController(SchoolManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetStudents(int page = 1, int pageSize = 10)
        {
            try
            {
                var students = await _context.Students
                     .Where(s => s.Status == 1)
                     .Select(s => new StudentDto
                     {
                         StudentId = s.StudentId,
                         Photo = s.Photo,
                         FirstName = s.FirstName,
                         FatherName = s.FahterName,
                         GrandFatherName = s.GrandFatherName,
                         SurName = s.SurName,
                         Gender = s.Gender,
                         NationalId = s.NationalId,
                         BirthDate = s.BirthDate,
                         JoinDate = s.JoinDate,
                         BloodType = s.BloodType,
                         YearClassID = s.YearClassID,
                         Username = s.Username,
                         Email = s.Email,
                         Password = s.Password,
                         RegionId = s.RegionId,
                         Address = s.Address,
                         ParentName = s.ParentName,
                         ParentPhone = s.ParentPhone,
                         ParentEmail = s.ParentEmail,
                         CreatedOn = s.CreatedOn,
                         CreatedBy = s.CreatedBy,
                         Status = s.Status
                     })
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post api/Students
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentPostDto student)
        {
            try
            {

                if (student == null)
                {
                    return StatusCode(404, "خطأ في عملية ارسال البيانات");
                }


                
                if (!Validations.IsValidPhone(student.ParentPhone))
                {
                    return StatusCode(404, "Parent's Phonenumber is not valid !!");
                }

                if (!Validations.IsValidEmail(student.ParentEmail))
                {
                    return StatusCode(404, "Parent's email is not valid !!");
                }

                if (!Validations.IsValidEmail(student.Email))
                {
                    return StatusCode(404, "Student's email is not valid !!");
                }
                if (!Validations.IsValidPassword(student.Password))
                {
                    return StatusCode(404, "Password can't be null or less than 9 letters must be mixed with letters and numbers and characters !!");
                }
                Student studentobj = new Student();
                studentobj.Photo = student.Photo;
                studentobj.FirstName = student.FirstName;
                studentobj.FahterName = student.FahterName;
                studentobj.GrandFatherName = student.GrandFatherName;
                studentobj.SurName = student.SurName;
                studentobj.Gender = student.Gender;
                studentobj.NationalId = student.NationalId;
                studentobj.BirthDate = student.BirthDate;
                studentobj.JoinDate = student.JoinDate;
                studentobj.BloodType = student.BloodType;
                studentobj.YearClassID =(student.YearClassID == 0 ? null : student.YearClassID);
                studentobj.Username = student.Username;
                studentobj.Email = student.Email;
                studentobj.Password = student.Password;
                studentobj.RegionId = (student.RegionId==0 ? null:student.RegionId);
                studentobj.Address = student.Address;
                studentobj.ParentName = student.ParentName;
                studentobj.ParentPhone = student.ParentPhone;
                studentobj.ParentEmail = student.ParentEmail;
                studentobj.CreatedOn = DateTime.Now;
                studentobj.CreatedBy = null;
                studentobj.Status = 1;

                _context.Students.Add(studentobj);
                await _context.SaveChangesAsync();

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update api/Students
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(long id, [FromBody] StudentPutDto student)
        {
            if (id != student.StudentId)
            {
                return BadRequest("The student was not found.");
            }

            try
            {
                var studentToUpdate = await _context.Students.FindAsync(id);

                if (studentToUpdate == null)
                {
                    return BadRequest("The student was not found.");
                }

                studentToUpdate.Photo = student.Photo;
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.FahterName = student.FahterName;
                studentToUpdate.GrandFatherName = student.GrandFatherName;
                studentToUpdate.SurName = student.SurName;
                studentToUpdate.Gender = student.Gender;
                studentToUpdate.NationalId = student.NationalId;
                studentToUpdate.BirthDate = student.BirthDate;
                studentToUpdate.JoinDate = student.JoinDate;
                studentToUpdate.BloodType = student.BloodType;
                studentToUpdate.YearClassID = (student.YearClassID == 0 ? null : student.YearClassID);
                studentToUpdate.Username = student.Username;
                studentToUpdate.Email = student.Email;
                studentToUpdate.Password = student.Password;
                studentToUpdate.RegionId = (student.RegionId == 0 ? null : student.RegionId);
                studentToUpdate.Address = student.Address;
                studentToUpdate.ParentName = student.ParentName;
                studentToUpdate.ParentPhone = student.ParentPhone;
                studentToUpdate.ParentEmail = student.ParentEmail;
                studentToUpdate.UpdatedOn = DateTime.Now;
                studentToUpdate.UpdatedBy = null;
                studentToUpdate.Status = 1;

                _context.Entry(studentToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return Ok("The student was updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Students
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            var studentToDelete = await _context.Students.FindAsync(id);

            if (studentToDelete == null)
            {
                return NotFound("The student was not found.");
            }
            try
            {
                studentToDelete.Status = 9;
                studentToDelete.UpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok("The student was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}