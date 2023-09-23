using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Category
    {
        public Category()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int CategoryId { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
