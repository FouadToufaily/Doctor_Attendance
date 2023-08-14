using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Department
    {
        public Department()
        {
            Attendances = new HashSet<Attendance>();
            Doctors = new HashSet<Doctor>();
            Employees = new HashSet<Employee>();
            Faculties = new HashSet<Faculty>();
        }

        public int DepId { get; set; }
        public int? DoctorId { get; set; }
        public string? DepName { get; set; }
        public int? Nbdoctors { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
