using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderReq, Order>();
        CreateMap<OrderItemReq, OrderDetail>();
    }
}
