using System.ComponentModel.DataAnnotations;

namespace API.Types.Objects;

public class CreateCategoryReq
{
    [Required] public string Description { get; set; }
}

public class UpdateCategoryReq
{
    public string? Description { get; set; }
}

public class CategoryRes
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }
}
