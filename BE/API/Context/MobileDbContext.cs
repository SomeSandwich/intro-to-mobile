using Api.Context.Entities;
using Api.Context.GenerateData;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class MobileDbContext : DbContext
{
    public MobileDbContext(DbContextOptions<MobileDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        FakerGenerating.Init();

        new UserDetailConfiguration().Configure(builder.Entity<User>());
        new CategoryConfiguration().Configure(builder.Entity<Category>());
        new PostDetailConfiguration().Configure(builder.Entity<Post>());
        new RateDetailConfiguration().Configure(builder.Entity<Rate>());
        new ReportDetailConfiguration().Configure(builder.Entity<Report>());
        new CartConfiguration().Configure(builder.Entity<Cart>());
        new ConversationConfiguration().Configure(builder.Entity<Conversation>());
        new ParticipationConfiguration().Configure(builder.Entity<Participation>());
        new MessageConfiguration().Configure(builder.Entity<Message>());
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

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    #endregion
}
