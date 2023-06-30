using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj439.Model;

public partial class Attendence
{
    public int DepId { get; set; }

    public int DoctorId { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AttId { get; set; }

    public DateTime? Date { get; set; }

    public int? NbHours { get; set; }

    public string? Comments { get; set; }

    public bool? Attend { get; set; }

    public virtual Department Dep { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;
}
