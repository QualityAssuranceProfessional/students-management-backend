using API.DTOs;
using API.Module;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace API.Controllers
{
    
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class TeachersController : Controller
    {
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public TeachersController(SchoolManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<IActionResult> GetTeachers(int page = 1, int pageSize = 10)
        {
            try
            {
                var teachers = await _context.Teachers
                    .Where(t => t.Status == 1) // Filter teachers with Status = 1
                    .OrderByDescending(t => t.CreatedOn)
                    .Select(t => new TeacherDto
                    {
                        TeacherId = t.TeacherId,
                        FirstName = t.FirstName,
                        FatherName = t.FatherName,
                        GrandFatherName = t.GrandFatherName,
                        SurName = t.SurName,
                        Gender = t.Gender,
                        NationalId = t.NationalId,
                        JoinDate = t.JoinDate,
                        Specialization = t.Specialization,
                        RegionId = t.RegionId,
                        Address = t.Address,
                        Username = t.Username,
                        Email = t.Email,
                        CreatedOn = t.CreatedOn,
                        Status = t.Status
                    })
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Post api/Teachers
        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherPostDto teacher)
        {
            try
            {
                if (teacher == null)
                {
                    return BadRequest("The teacher data is missing or invalid.");
                }

                var passwordValidation = Validations.IsValidPassword(teacher.Password);
                if (!passwordValidation)
                {
                    return BadRequest("Password complexity requirements not met. Details: " + string.Join(" ", passwordValidation));
                }

                if (!Validations.IsValidEmail(teacher.Email))
                {
                    return BadRequest("Invalid email address.");
                }

                //if (!Validations.IsStringOnly(teacher.FirstName))
                //{
                //    return BadRequest("this fields should only string");
                //}

                if (string.IsNullOrEmpty(teacher.FirstName))
                {
                    return BadRequest("FirstName should not be null !!");
                }

                var teacherobj = new Teacher
                {
                    FirstName = teacher.FirstName,
                    FatherName = teacher.FatherName,
                    GrandFatherName = teacher.GrandFatherName,
                    SurName = teacher.SurName,
                    Gender = teacher.Gender,
                    NationalId = teacher.NationalId,
                    JoinDate = teacher.JoinDate,
                    Specialization = teacher.Specialization,
                    RegionId = teacher.RegionId,
                    Address = teacher.Address,
                    Username = teacher.Username,
                    Email = teacher.Email,
                    Password = Security.ComputeHash(teacher.Password, HashAlgorithms.SHA256, null),
                    CreatedOn = DateTime.Now,
                    CreatedBy = null,
                    Status = 1
                };

                _context.Teachers.Add(teacherobj);
                await _context.SaveChangesAsync();

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // update api/Teachers

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherPutDto teacher)
        {

            try
            {
                if (id != teacher.TeacherId)
                {
                    return BadRequest("The teacher ID in the route does not match the teacher ID in the request body.");
                }

                if (String.IsNullOrEmpty(teacher.FirstName))
                {
                    return BadRequest("The first name of the teacher is required.");
                }

                if (!Validations.IsValidEmail(teacher.Email))
                {
                    return BadRequest("Invalid Email address.");
                }

                var passwordValidation = Validations.IsValidPassword(teacher.Password);
                if (!passwordValidation)
                {
                    return BadRequest("Password complexity requirements not met. Details: " + string.Join(" ", passwordValidation));
                }

                var teacherToUpdate = await _context.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);

                if (teacherToUpdate == null)
                {
                    return NotFound("The teacher was not found.");
                }

                teacherToUpdate.FirstName = teacher.FirstName;
                teacherToUpdate.FatherName = teacher.FatherName;
                teacherToUpdate.GrandFatherName = teacher.GrandFatherName;
                teacherToUpdate.SurName = teacher.SurName;
                teacherToUpdate.NationalId = teacher.NationalId;
                teacherToUpdate.JoinDate = teacher.JoinDate;
                teacherToUpdate.Specialization = teacher.Specialization;
                teacherToUpdate.RegionId = teacher.RegionId;
                teacherToUpdate.Address = teacher.Address;
                teacherToUpdate.Username = teacher.Username;
                teacherToUpdate.Email = teacher.Email;
                teacherToUpdate.Password = teacher.Password;
                teacherToUpdate.UpdatedOn = DateTime.Now;
                teacherToUpdate.UpdatedBy = null;
                teacherToUpdate.Status = 1;

                await _context.SaveChangesAsync();

                return Ok("The teacher was updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // delete api/teacher
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(long id) 
        {
            var teacherToDelete = await _context.Teachers.Where(x => x.Status != 9 && x.TeacherId == id).SingleOrDefaultAsync();

            if (teacherToDelete == null)
            {
                return NotFound("The teacher was not found or Already deleted before.");
            }

            try
            {
                teacherToDelete.Status = 9;
                teacherToDelete.UpdatedOn = DateTime.Now;
                teacherToDelete.UpdatedBy = null;
                await _context.SaveChangesAsync();

                return Ok("The teacher was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}

