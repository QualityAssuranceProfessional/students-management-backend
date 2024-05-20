using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class User
    {
        public int UserId { get; set; }
   
        public Byte[] Photo { get; set; }
        public string? Email { get; set; }
        public String? Password { get; set; }
        public string? FullName { get; set; }
        public String? UserName { get; set; }
        public String? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? LoginTryAttemptDate { get; set; }
        public short? LoginTryAttempts { get; set; }
        public DateTime? LastLoginOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
       
        public short? Status { get; set; }
    }
}
