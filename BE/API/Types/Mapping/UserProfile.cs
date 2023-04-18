using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserReq, User>();
        CreateMap<User, SellerRes>();
        CreateMap<User, UserRes>();
    }
}
