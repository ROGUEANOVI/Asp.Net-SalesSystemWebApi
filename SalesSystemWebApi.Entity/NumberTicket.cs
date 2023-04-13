using System;
using System.Collections.Generic;

namespace SalesSystemWebApi.Entity;

public partial class NumberTicket
{
    public int NumberTicketId { get; set; }

    public int LastNumber { get; set; }

    public DateTime? RegistrationDate { get; set; }
}
