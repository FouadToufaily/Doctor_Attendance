using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Doctor_Attendance.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Deps = new HashSet<Department>();
            Sections = new HashSet<Section>();
        }

        public int Facultyid { get; set; }
        public int? DoctorId { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Head of Faculty")]
        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<Department> Deps { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
