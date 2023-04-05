using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostReq, Api.Context.Entities.Post>();
        CreateMap<Api.Context.Entities.Post, GetPostRes>();
    }
}
