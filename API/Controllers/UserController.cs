using API.DTOs;
using API.Module;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/[controller]")]
    public class UserController : Controller
    {
        
        // constructor with dbcontext
        private readonly SchoolManagementSystemContext _context;
        public UserController(SchoolManagementSystemContext context)  
        {
            _context = context;
            

            
        }
        

        // Get api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int pageSize = 10)
        {
            try
            {
                var Users = await _context.Users
                      .Where(u => u.Status == 1)
                      .Select(u => new UserDto
                      {
                          UserId = u.UserId,
                          UserName = u.UserName,
                          CreatedOn = u.CreatedOn,
                      })
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();

                return Ok(Users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post api/Users
        [HttpPost]
        public async Task<IActionResult> AddUsers([FromBody] UserPostDto User)
        {
            try
            {

                User userobj = new User
                {
                    Photo = User.Photo,
                    Email = User.Email,
                    Password = User.Password,
                    FullName = User.FullName,
                    UserName = User.UserName,
                    PhoneNumber = User.PhoneNumber,
                    BirthDate = User.BirthDate,
                    CreatedOn = DateTime.Now,
                    CreatedBy = null,
                    Status = 1,
                };
              
                if (!Validations.IsValidPhone(userobj.PhoneNumber))
                {
                    return StatusCode(404, "Parent's Phonenumber is not valid !!");
                }

                if (!Validations.IsValidEmail(userobj.Email))
                {
                    return StatusCode(404, "Parent's email is not valid !!");
                }


                _context.Users.Add(userobj);
                await _context.SaveChangesAsync();

                return Ok("Validations is success !!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update api/Users
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] UserPutDto user)
        {
            if (id != user.UserId)
            {
                return BadRequest("The User was not found.");
            }

            try
            {
                var UserToUpdate = await _context.Users.FindAsync(id);

                if (UserToUpdate == null)
                {
                    return BadRequest("The User was not found.");
                }

                //UserToUpdate.Photo = user.Photo;    
                UserToUpdate.Email = user.Email;
                UserToUpdate.Password = user.Password;
                UserToUpdate.FullName = user.FullName;
                UserToUpdate.UserName = user.UserName;
                UserToUpdate.PhoneNumber = user.PhoneNumber;
                UserToUpdate.BirthDate = user.BirthDate;
                UserToUpdate.UpdatedOn = DateTime.Now;
                UserToUpdate.UpdatedBy = null;
                UserToUpdate.Status = 1;
                

                _context.Entry(UserToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("The User was updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Users
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);

            if (userToDelete == null)
            {
                return NotFound("The User was not found.");
            }
            try
            {
                userToDelete.Status = 9;
                userToDelete.UpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok("The User was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
