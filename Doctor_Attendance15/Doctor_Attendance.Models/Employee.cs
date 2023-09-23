using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Users = new HashSet<User>();
        }

        public int EmpId { get; set; }
        public int? DepId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }

        public virtual Department? Dep { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
