using System.ComponentModel.DataAnnotations;

namespace API.Types.Objects;

#region REQUEST

public class MakeBucketDto
{
    [Required] public string Bucket { get; set; }
}

#endregion

#region RESPONSE

public class ResBucketStat
{
    public string Name { get; set; }
    public DateTimeOffset CreateTimeInUTC { get; set; }
}

#endregion
