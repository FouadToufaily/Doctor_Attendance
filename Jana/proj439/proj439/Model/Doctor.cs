using System;
using System.Collections.Generic;

namespace proj439.Model;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int? CategoryId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Attendence> Attendences { get; set; } = new List<Attendence>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Department> Deps { get; set; } = new List<Department>();
}
