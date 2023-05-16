using System.Text.Json.Serialization;
using Api.Context.Constants.Enums;
using Api.Context.Entities;

namespace API.Types.Objects;

public class OrderItemReq
{
    public int PostId { get; set; }

    public int UnitPrice { get; set; }
}

public class CreateOrderReq
{
    [JsonIgnore] public int CustomerId { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public List<OrderItemReq> Details { get; set; }
}

public class UpdateOrderAddressReq
{
    [JsonIgnore] public int CustomerId { get; set; }
    public string? DeliveryAddress { get; set; }
}

public class UpdateOrderArg
{
    public string? DeliveryAddress { get; set; }

    public OrderStatus? Status { get; set; }

    public int? Total { get; set; }
}

public class OrderDetailRes
{
    public int OrderId { get; set; }

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

    public IEnumerable<OrderDetailRes> OrderDetails { get; set; }
}
