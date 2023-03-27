<<<<<<< HEAD
﻿using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
=======
﻿using System;
using System.Collections.Generic;
>>>>>>> f87de6b070b981742c3402a2b4c6ea9765cc3ee7

namespace library.Models.Entities;

public partial class User
{
    public int IdU { get; set; }
<<<<<<< HEAD
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
=======

    public string? Username { get; set; }

    public string? Password { get; set; }
>>>>>>> f87de6b070b981742c3402a2b4c6ea9765cc3ee7

    public string? Email { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? IdRole { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
