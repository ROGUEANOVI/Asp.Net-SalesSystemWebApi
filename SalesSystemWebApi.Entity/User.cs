﻿using System;
using System.Collections.Generic;

namespace SalesSystemWebApi.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public int? RolId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual Rol? Rol { get; set; }
}
