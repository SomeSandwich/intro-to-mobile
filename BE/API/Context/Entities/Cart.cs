using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Cart
{
    [ForeignKey("Post")] public int PostId { get; set; }
    public virtual Post Post { get; set; }


    [ForeignKey("User")] public int UserId { get; set; }
    public virtual User User { get; set; }
}

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => new { c.UserId, c.PostId });
    }
}