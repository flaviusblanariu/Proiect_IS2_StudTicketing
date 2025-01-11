using System;
using System.Collections.Generic;

namespace StudTicketing.Database.Models;

public partial class Category
{
    public int id { get; set; }
    
    public string Functie_persoana { get; set; } = null!;

    public string Tip_tichet { get; set; } = null!;

}