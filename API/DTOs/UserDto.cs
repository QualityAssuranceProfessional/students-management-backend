namespace API.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public String? UserName { get; set; }  
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public String? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public short? Status { get; set; }
    }
}
