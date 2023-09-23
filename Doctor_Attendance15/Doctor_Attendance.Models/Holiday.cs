using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor_Attendance.Models
{
    public partial class Holiday
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }

        public String DateOnly { get { return Date.ToShortDateString(); } }

    }
}
