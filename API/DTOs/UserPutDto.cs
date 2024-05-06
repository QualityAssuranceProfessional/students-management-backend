using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class UserPutDto
    {
        public int UserId { get; set; }
        

        public String? Photo { get; set; }
        public string? Email { get; set; }
        public String? Password { get; set; }
        public string? FullName { get; set; }
        public String? UserName { get; set; }
        public String? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
