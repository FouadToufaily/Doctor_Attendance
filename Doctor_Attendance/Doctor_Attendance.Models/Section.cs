using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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

        [Display(Name = "Head of Section")]
        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
