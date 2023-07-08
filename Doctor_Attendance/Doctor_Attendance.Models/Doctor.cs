using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Attendences = new HashSet<Attendence>();
            Departments = new HashSet<Department>();
            Faculties = new HashSet<Faculty>();
            Sections = new HashSet<Section>();
            Users = new HashSet<User>();
            Deps = new HashSet<Department>();
        }

        public int DoctorId { get; set; }
        public int? CategoryId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }

        public string Fullname { get { return this.Firstname + " " + this.Lastname; } }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Attendence> Attendences { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Department> Deps { get; set; }
    }
}
