using System;
using System.Collections.Generic;

namespace proj439.Model;

public partial class Section
{
    public int Sectionid { get; set; }

    public int? DoctorId { get; set; }

    public int? Number { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Location { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<Department> Deps { get; set; } = new List<Department>();

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
