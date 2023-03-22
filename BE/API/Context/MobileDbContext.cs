using System.Data.Common;
using Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class MobileDbContext : DbContext
{
    public MobileDbContext(DbContextOptions<MobileDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        new CartConfiguration().Configure(builder.Entity<Cart>());
        new ParticipationConfiguration().Configure(builder.Entity<Participation>());
        new OrderConfiguration().Configure(builder.Entity<Order>());
        new OrderDetailConfiguration().Configure(builder.Entity<OrderDetail>());
        
    }

    #region Entities

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    #endregion
}