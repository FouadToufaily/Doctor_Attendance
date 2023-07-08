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
            Sections = new HashSet<Section>();
        }

        public int Facultyid { get; set; }
        public int? DoctorId { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Doctor in Charge")]
        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
