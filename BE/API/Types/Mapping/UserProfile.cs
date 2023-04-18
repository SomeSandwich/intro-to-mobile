using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, SellerRes>();
        CreateMap<User, UserRes>();
    }
}
