using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Context.GenerateData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Price { get; set; }

    public string? Caption { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string[]? MediaPath { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsSold { get; set; }

    public bool IsHide { get; set; }

    public bool IsDeleted { get; set; }

    public Rate Rate { get; set; }

    public List<int> UserShare { get; set; } = new();

    public virtual ICollection<Report> Reports { get; set; }

    public virtual ICollection<Cart> Carts { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual OrderDetail OrderDetail { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey("Category")] public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

public class PostDetailConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasData(FakerGenerating.Posts);
    }
}
