using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Section
    {
        public Section()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int Sectionid { get; set; }
        public int? DoctorId { get; set; }
        public int? Number { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }

        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
