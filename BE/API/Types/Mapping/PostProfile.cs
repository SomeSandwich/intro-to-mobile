using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostReq, Post>();
        CreateMap<Post, PostRes>();
    }
}
