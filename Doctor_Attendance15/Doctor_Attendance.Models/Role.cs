using System;
using System.Collections.Generic;

namespace Doctor_Attendance.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public int? Permissionid { get; set; }
        public string? RoleName { get; set; }

        public virtual Permission? Permission { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
