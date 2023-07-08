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
            Deps = new HashSet<Department>();
            Faculties = new HashSet<Faculty>();
        }

        public int Sectionid { get; set; }
        public int? DoctorId { get; set; }
        public int? Number { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }

        [Display(Name = "Dean")]
        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<Department> Deps { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
