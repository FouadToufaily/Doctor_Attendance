using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int Permissionid { get; set; }
        public int? DeleteAttendence { get; set; }
        public int? UpdateAttendence { get; set; }
        public int? AddAttendence { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
