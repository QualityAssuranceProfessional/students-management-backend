using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class StudentsController : Controller
    {
        private readonly SchoolManagementSystemContext _context;
        public StudentsController(SchoolManagementSystemContext context)
        {
            _context = context;
        }



        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentPostDto student)
        {
            try
            {
                if (!Validations.IsValidPhone(student.ParentPhone)){
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


                return Ok("Validations is success !!");

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
