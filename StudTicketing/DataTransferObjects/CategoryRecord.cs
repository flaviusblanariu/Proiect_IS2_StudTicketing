namespace StudTicketing.DataTransferObjects;

public class CategoryRecord
{
    public int id { get; set; }
    
    public string Functie_persoana { get; set; } = null!;

    public string Tip_tichet { get; set; } = null!;
}