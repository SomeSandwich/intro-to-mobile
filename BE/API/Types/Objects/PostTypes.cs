using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace API.Types.Objects;

public class CreatePostReq
{
    [JsonIgnore] public int UserId { get; set; }

    [Required] public int CategoryId { get; set; }

    [Required] public int Price { get; set; }

    [Required] public string Caption { get; set; } = string.Empty;

    public string? Description { get; set; }

    public IFormFileCollection? MediaFiles { get; set; }
}

public class UpdatePostReq
{
    public int? Price { get; set; }

    public string? Caption { get; set; }

    public string? Description { get; set; }

    public ICollection<string>? MediaFilesDelete { get; set; }

    public IFormFileCollection? MediaFilesAdd { get; set; }
}

public class CommentPostReq
{
    public string Content { get; set; }
}

public class UpdatePostArgs
{
    public int? Price { get; set; }

    public string? Caption { get; set; }

    public string? Description { get; set; }

    public ICollection<string>? MediaFilesDelete { get; set; }

    public ICollection<string>? MediaFilesAdd { get; set; }
}

public class PostRes
{
    public int Id { get; set; }

    public int Price { get; set; }

    public string? Caption { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string[]? MediaPath { get; set; }

    public DateTime CreatedDate;

    public DateTime UpdatedDate { get; set; }

    public bool IsSold { get; set; } = false;

    public RateRes Rate { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }

    public List<int> UserShare { get; set; }

    public IEnumerable<ReportRes> Reports { get; set; }
}

public class SearchPostReq
{
    public string Query { get; set; }
}
