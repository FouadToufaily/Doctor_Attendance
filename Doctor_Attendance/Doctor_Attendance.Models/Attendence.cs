using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Attendence
    {
        public int DepId { get; set; }
        public int DoctorId { get; set; }
        public int? AttId { get; set; }
        public DateTime? Date { get; set; }
        public int? NbHours { get; set; }
        public string? Comments { get; set; }

        public virtual Department Dep { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
