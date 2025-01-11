using System;
using System.Collections.Generic;

namespace StudTicketing.Database.Models;

public partial class Ticket
{
    public int id { get; set; }

    public string Tip_tichet { get; set; } = null!;

    public string Severitate { get; set; } = null!;

    public string Stadiu { get; set; } = null!;

}