using System;
using System.Collections.Generic;

namespace SalesSystemWebApi.Entity;

public partial class Sale
{
    public int SaleId { get; set; }

    public string? SaleTicketNumber { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Total { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; } = new List<SaleDetail>();
}
