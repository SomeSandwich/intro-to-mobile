using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Context.Constants.Enums;
using API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    [Range(-1, 10)] public double Legit { get; set; } = -1;

    public string Address { get; set; }

    public UserStatus Status { get; set; } = UserStatus.Default;


    public virtual ICollection<Post> Posts { get; set; }

    public virtual ICollection<Rate> Rates { get; set; }

    public virtual ICollection<Report> Reports { get; set; }

    public virtual ICollection<Cart> Carts { get; set; }

    public virtual ICollection<Participation> Participations { get; set; }

    public virtual ICollection<Message> Messages { get; set; }

    public virtual ICollection<Order> CustomerOrders { get; set; }

    public virtual ICollection<Order> SellerOrders { get; set; }
}

public class UserDetailConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = 1,
                Name = "Root",
                Email = "intro_to_mobile_root@gmail.com",
                PhoneNumber = "0905473034",
                PasswordHash = "123".HashPassword(),
                Legit = 10,
                Address = "Trái Đất"
            }, new User
            {
                Id = 2,
                Name = "Hiếu Nguyễn",
                Email = "hieucckha@gmail.com",
                PhoneNumber = "0905473034",
                PasswordHash = "123".HashPassword(),
                Legit = 10,
                Address = "Trái Đất"
            }
        );
    }
}
