using System;
using System.Collections.Generic;

namespace proj439.Model;

public partial class Faculty
{
    public int Facultyid { get; set; }

    public int? DoctorId { get; set; }

    public string? Name { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
