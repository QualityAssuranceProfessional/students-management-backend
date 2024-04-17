using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class User
    {
        public int UserId { get; set; }
        public byte[]? Photo { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public string? FullName { get; set; }
        public byte[]? UserName { get; set; }
        public byte[]? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }
    }
}
