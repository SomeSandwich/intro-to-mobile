using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<CreateUserReq, User>();
        CreateMap<CreateUserReq, Api.Context.Entities.User>();
    }
}
