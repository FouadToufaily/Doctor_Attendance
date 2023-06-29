using System;
using System.Collections.Generic;

namespace proj439.Model;

public partial class Permission
{
    public int Permissionid { get; set; }

    public int? DeleteAttendence { get; set; }

    public int? UpdateAttendence { get; set; }

    public int? AddAttendence { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
