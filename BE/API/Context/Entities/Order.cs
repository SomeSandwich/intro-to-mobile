using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Api.Context.Constants.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Total { get; set; }

    public string DeliveryAddress { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.OrderPlaced;


    public virtual ICollection<OrderDetail> OrderDetail { get; set; }


    [ForeignKey("Customer")] public int CustomerId { get; set; }
    public virtual User Customer { get; set; }

    [ForeignKey("Seller")] public int SellerId { get; set; }
    public virtual User Seller { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.Customer)
            .WithMany(e => e.CustomerOrders)
            .HasForeignKey(o => o.CustomerId);

        builder.HasOne(o => o.Seller)
            .WithMany(e => e.SellerOrders)
            .HasForeignKey(o => o.SellerId);
    }
}
