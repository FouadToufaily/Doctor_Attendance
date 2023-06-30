using System;
using System.Collections.Generic;

namespace proj439.Model;

public partial class Department
{
    public int DepId { get; set; }

    public int? DoctorId { get; set; }

    public int? Number { get; set; }

    public int? Nbdoctors { get; set; }

    public virtual ICollection<Attendence> Attendences { get; set; } = new List<Attendence>();

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
