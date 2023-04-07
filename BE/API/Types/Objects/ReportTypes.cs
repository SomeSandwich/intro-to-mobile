namespace API.Types.Objects;

public class ReportRes
{
    public int Id { get; set; }

    public string Reason { get; set; }

    public DateTime ReportAt { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }
}
