using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Context.Constants.Enums;
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

    public string? AvatarPath { get; set; }

    public string PasswordHash { get; set; }

    [Range(-1, 10)] public double Legit { get; set; } = -1;

    public string Address { get; set; }

    [DefaultValue(0)] public double Money { get; set; } = 0;

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
                Name = "Hiếu Nguyễn",
                Email = "hieucckha@gmail.com",
                PhoneNumber = "0905473034",
                AvatarPath = "avatar/UkmH7xfQ7o-3tY",
                PasswordHash = "1000:4kNP6sZVLX7Vx9KEJmt5q3fw2J6nBeIr:Td0uNIP1j4c94QG69342a7wzQmA=",
                Address = "Trái đất",
                Money = 69_000_000
            },
            new User
            {
                Id = 2,
                Name = "Hoàng Quốc Bảo",
                Email = "baokyo002@gmail.com",
                PhoneNumber = "8888888888",
                AvatarPath = "avatar/yH_rHI5emLoaxj",
                PasswordHash = "1000:AlY5lZ/XVS7E672SDgD3JHEwgS2qd+nY:wnetcKKMCMM7HGmw0lKSz+qP5kI=",
                Address = "Trái đất",
                Money = 69_000_000
            },
            new User
            {
                Id = 3,
                Name = "Trương Trọng Khánh",
                Email = "truongtrongkhanh@gmail.com",
                PhoneNumber = "0888666777",
                AvatarPath = "avatar/yH_rHI5emLoaxj",
                PasswordHash = "1000:NXww47Xz9tojPeccOvGaGD1zoYXtmfLM:bk/K2fumHFMZDqgSeJfbQdfVFU0=",
                Address = "Trái đất",
                Money = 69_000_000
            },
            new User
            {
                Id = 4,
                Name = "Test User 1",
                Email = "test1@gmail.com",
                PhoneNumber = "0111111111",
                AvatarPath = "avatar/EBEpCr1wxwGSmx",
                PasswordHash = "1000:ymwCDpQT4QSel9gtWNwwIKGgvAdwPZkl:P84yWO1AB6JnQzY3dZ/bp446NnA=",
                Address = "Trái đất",
                Money = 10_000_000
            },
            new User
            {
                Id = 5,
                Name = "Test User 2",
                Email = "test2@gmail.com",
                PhoneNumber = "0222222222",
                AvatarPath = "avatar/EBEpCr1wxwGSmx",
                PasswordHash = "1000:3Ve21SeYEYzGngFUbYVrk9WJmVwiqzy/:Hs2oxtyFwkqGbtiG3Khn3pTH1Lo=",
                Address = "Trái đất",
                Money = 0
            }
        );
    }
}
