using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Context.Constants.Enums;

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

    public int Legit { get; set; } = 0;

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