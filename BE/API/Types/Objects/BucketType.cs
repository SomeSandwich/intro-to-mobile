using System.ComponentModel.DataAnnotations;

namespace API.Types.Objects;

public class MakeBucketDto
{
    [Required] public string Bucket { get; set; }
}

public class ResBucketStat
{
    public string Name { get; set; }
    public DateTimeOffset CreateTimeInUTC { get; set; }
}
