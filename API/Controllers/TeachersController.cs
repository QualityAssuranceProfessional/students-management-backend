﻿using API.DTOs;
using API.Module;
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
                        Password = t.Password,
                        CreatedOn = t.CreatedOn,
                        CreatedBy = t.CreatedBy,
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
                // Check if the teacher parameter is null
                if (teacher == null)
                {
                    return BadRequest("The teacher data is missing or invalid.");
                }

                // Validate email
                if (!Validations.IsValidEmail(teacher.Email))
                {
                    return BadRequest("Invalid email address.");
                }

                // Create a new Teacher object and map properties from TeacherPostDto
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
                    Password = teacher.Password,
                    CreatedOn = DateTime.Now,
                    CreatedBy = null, // You may need to set this value based on the current user
                    Status = 1 // You may need to set the initial status
                };

                // Add the new teacher to the database and save changes
                _context.Teachers.Add(teacherobj);
                await _context.SaveChangesAsync();

                // Return a successful response with the added teacher object
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a bad request response with the error message
                return BadRequest(ex.Message);
            }
        }

        // update api/Teachers


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] TeacherPutDto teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest("The teacher was not found.");
            }

            if (String.IsNullOrEmpty(teacher.FirstName))
            {
                return StatusCode(404, "the teacher is empty !!");
            }

            try
            {
                var teacherToUpdate = await _context.Teachers.Where(x => x.TeacherId == teacher.TeacherId).SingleOrDefaultAsync();

                if (teacherToUpdate == null)
                {
                    return BadRequest("The teacher was not found.");
                }

                teacherToUpdate.FirstName = teacher.FirstName;
                teacherToUpdate.FatherName = teacher.FatherName;
                teacherToUpdate.GrandFatherName= teacher.GrandFatherName;
                teacherToUpdate.SurName= teacher.SurName;
                teacherToUpdate.NationalId= teacher.NationalId;
                teacherToUpdate.JoinDate=teacher.JoinDate;
                teacherToUpdate.Specialization= teacher.Specialization;
                teacherToUpdate.RegionId= teacher.RegionId;
                teacherToUpdate.Address= teacher.Address;
                teacherToUpdate.Username= teacher.Username;
                teacherToUpdate.Email= teacher.Email;
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
            var teacherToDelete = await _context.Teachers.FindAsync(id);

            if (teacherToDelete == null)
            {
                return NotFound("The teacher was not found.");
            }

            try
            {
                teacherToDelete.Status = 9;
                teacherToDelete.UpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok("The teacher was deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /* [HttpDelete("{id}")]
         public async Task<IActionResult> Deleteteacher(int id)
         {
             var teacherToDelete = await _context.Teachers.FindAsync(id);

             if (teacherToDelete == null)
             {
                 return NotFound("The teacher was not found.");
             }

             try
             {
                 teacherToDelete.Status = 9;
                 teacherToDelete.UpdatedOn = DateTime.Now;
                 await _context.SaveChangesAsync();

                 return Ok("The teacher was deleted successfully.");
             }
             catch (Exception ex)    
             {
                 return BadRequest(ex.Message);
             }
         }*/
    }
}
