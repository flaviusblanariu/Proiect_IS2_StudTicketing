namespace StudTicketing.DataTransferObjects;

public class TicketUpdateRecord
{
    public int id { get; set; }

    public string Tip_tichet { get; set; } = null!;

    public string Severitate { get; set; } = null!;

    public string Stadiu { get; set; } = null!;
}