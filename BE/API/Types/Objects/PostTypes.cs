using System.ComponentModel.DataAnnotations;
using Api.Context.Entities;
using Newtonsoft.Json;

namespace API.Types.Objects;

#region Post

public class CreatePostReq
{
    [JsonIgnore] public int UserId { get; set; }

    [Required] public int CategoryId { get; set; }

    [Required] public int Price { get; set; }

    [Required] public string Caption { get; set; } = string.Empty;

    public string? Description { get; set; }

    public IFormFileCollection MediaFiles { get; set; }
}

#endregion

public class UpdatePostReq
{
    public int? Price { get; set; }

    public string? Caption { get; set; }

    public string? Description { get; set; }

    public ICollection<string>? MediaFilesDelete { get; set; }

    public IFormFileCollection? MediaFilesAdd { get; set; }
}

public class UpdatePostArgs
{
    public int? Price { get; set; }

    public string? Caption { get; set; }

    public string? Description { get; set; }

    public ICollection<string>? MediaFilesDelete { get; set; }

    public ICollection<string>? MediaFilesAdd { get; set; }
}

public class GetPostRes
{
    public int Id { get; set; }

    public int Price { get; set; }

    public string? Caption { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string[]? MediaPath { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsSold { get; set; } = false;


    public virtual Rate Rate { get; set; }

    public virtual ICollection<Report> Reports { get; set; }
}
