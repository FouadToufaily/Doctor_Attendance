using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Department
    {
        public Department()
        {
            Attendences = new HashSet<Attendence>();
            Employees = new HashSet<Employee>();
            Doctors = new HashSet<Doctor>();
            Sections = new HashSet<Section>();
        }

        public int DepId { get; set; }
        public int? DoctorId { get; set; }
        public int? Number { get; set; }
        public int? Nbdoctors { get; set; }
        public string? DepName { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Attendence> Attendences { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
