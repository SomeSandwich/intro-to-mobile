using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<CreateUserReq, Api.Context.Entities.User>();
    }
}
