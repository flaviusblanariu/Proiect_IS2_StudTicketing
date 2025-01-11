using System;
using System.Collections.Generic;

namespace StudTicketing.Database.Models;

public partial class Users
{
    public int id { get; set; }

    public string Nume { get; set; } = null!;

    public string Prenume { get; set; } = null!;

    public string Functie { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string Email { get; set; } = null!;
}