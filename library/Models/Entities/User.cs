using System;
using System.Collections.Generic;

namespace library.Models.Entities;

public partial class User
{
    public int IdU { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? IdRole { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
