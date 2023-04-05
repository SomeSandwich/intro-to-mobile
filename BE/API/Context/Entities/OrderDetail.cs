using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Context.Entities;

public class OrderDetail
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ForeignKey("Order")] public int OrderId { get; set; }

    [ForeignKey("Post")] public int PostId { get; set; }

    public int UnitPrice { get; set; }

    public virtual Post Post { get; set; }

    public virtual Order Order { get; set; }
}

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(od => new { od.OrderId, od.PostId });
    }
}
