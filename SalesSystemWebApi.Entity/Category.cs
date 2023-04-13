using System;
using System.Collections.Generic;

namespace SalesSystemWebApi.Entity;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
