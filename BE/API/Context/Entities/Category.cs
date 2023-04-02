using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Description { get; set; }


    public virtual ICollection<Post> Posts { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category { Id = 1, Description = "Áo" },
            new Category { Id = 2, Description = "Quần" },
            new Category { Id = 3, Description = "Váy" },
            new Category { Id = 4, Description = "Áo khoác" },
            new Category { Id = 5, Description = "Mũ" }
        );
    }
}
