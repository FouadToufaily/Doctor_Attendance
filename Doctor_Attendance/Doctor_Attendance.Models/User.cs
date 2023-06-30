using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class User
    {
        public int EmpId2 { get; set; }
        public int? DoctorId { get; set; }
        public int? EmpId { get; set; }
        public int? Permissionid { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public DateTime? Datecreated { get; set; }
        public DateTime? Lastmodified { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Employee? Emp { get; set; }
        public virtual Permission? Permission { get; set; }
    }
}
