using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Attendance
    {
        public int AttId { get; set; }
        public int DepId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int? NbHours { get; set; }
        public bool? Attended { get; set; }
        public bool? Published { get; set; }
        public string? Comments { get; set; }

        public String DateOnly { get { return Date.ToShortDateString(); } }


        public virtual Department Dep { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
