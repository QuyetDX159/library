using System;
using System.Collections.Generic;

namespace library.Models.Entities;

public partial class Role
{
    public int IdRole { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
