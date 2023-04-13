using System;
using System.Collections.Generic;

namespace SalesSystemWebApi.Entity;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; } = new List<SaleDetail>();
}
