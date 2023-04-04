using System.Text.Json.Serialization;
using Api.Context.Constants.Enums;
using Api.Context.Entities;

namespace API.Types.Objects;

public class CreateOrderReq
{
    [JsonIgnore]
    public int CustomerId { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public ICollection<Item> Details { get; set; }
}

public class Item
{
    public int PostId { get; set; }

    public int UnitPrice { get; set; }
}

public class OrderRes
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int SellerId { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public OrderStatus Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}

public class UpdateOrderAddressReq
{
    public string? DeliveryAddress { get; set; }
}

public class UpdateOrderArg
{
    public string? DeliveryAddress { get; set; }

    public OrderStatus? Status { get; set; }

    public int? Total { get; set; }
}

// public class OrderDetailRes
// {
//     public int PostId { get; set; }
//
//     public int OrderId { get; set; }
//
//     public int UnitPrice { get; set; }
//
//     public virtual Post Post { get; set; }
// }
