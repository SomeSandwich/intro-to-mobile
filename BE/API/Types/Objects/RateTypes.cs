namespace API.Types.Objects;

public class RateRes
{
    public int Id { get; set; }

    public DateTime RatingAt { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }
}
