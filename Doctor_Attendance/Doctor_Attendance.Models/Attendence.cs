using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Doctor_Attendance.Models
{
    public partial class Attendence
    {
        public int AttId { get; set; }
        public int DepId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Hours Given")]
        public int? NbHours { get; set; }
        public string? Comments { get; set; }

        public virtual Department Dep { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
