using System;
using System.Collections.Generic;

namespace proj439.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
