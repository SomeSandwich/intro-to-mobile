using Api.Context.Entities;
using AutoMapper;

namespace API.Types.Mapping;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Order, Order>();
    }
}
