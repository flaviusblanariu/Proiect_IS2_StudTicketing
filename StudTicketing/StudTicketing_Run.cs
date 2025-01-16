namespace StudTicketing;

public class StudTicketing_Run
{
    public int TicketId { get; set; }

    public string Title { get; set; }

    public string Department { get; set; }

    public TicketStatus Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ResolvedDate { get; set; }

    public int DaysOpen => (ResolvedDate ?? DateTime.Now).Subtract(CreatedDate).Days;

    public string? Summary { get; set; }
}

public enum TicketStatus
{
    New,
    InProgress,
    Resolved,
    Closed
}