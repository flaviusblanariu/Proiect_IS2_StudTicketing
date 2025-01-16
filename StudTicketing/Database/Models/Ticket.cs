using System;
using System.Collections.Generic;

namespace StudTicketing.Database.Models;

public partial class Ticket
{
    public int id { get; set; }

    public string Tip_tichet { get; set; } = null!;

    public string Severitate { get; set; } = null!;

    public string Stadiu { get; set; } = null!;
    public int Id { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}