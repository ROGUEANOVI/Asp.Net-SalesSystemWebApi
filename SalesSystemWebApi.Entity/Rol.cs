using System;
using System.Collections.Generic;

namespace SalesSystemWebApi.Entity;

public partial class Rol
{
    public int RolId { get; set; }

    public string? Name { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; } = new List<MenuRol>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
