using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int? DoctorId { get; set; }
        public int? EmpId { get; set; }
        public int? RoleId { get; set; }
        public string? UserUsername { get; set; }
        public string? UserPassword { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Employee? Emp { get; set; }
        public virtual Role? Role { get; set; }
    }
}
