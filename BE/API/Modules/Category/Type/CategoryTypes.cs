using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Category.Type;

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
}
