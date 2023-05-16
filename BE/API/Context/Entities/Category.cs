using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Api.Context.GenerateData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }


    public virtual ICollection<Post> Posts { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category { Id = 1, Description = "Áo", Icon = "category/shirt.png" },
            new Category { Id = 2, Description = "Quần", Icon = "category/pant.png" },
            new Category { Id = 3, Description = "Váy", Icon = "category/skirt.png" },
            new Category { Id = 4, Description = "Áo khoác", Icon = "category/jacket.png" },
            new Category { Id = 5, Description = "Mũ", Icon = "category/hat.png" }
        );
    }
}
