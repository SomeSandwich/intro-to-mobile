using Api.Modules.Post.Services;
using Api.Modules.Post.Type;
using AutoMapper;

namespace Api.Modules.Post.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<ReqCreatePost, Context.Entities.Post>();
        CreateMap<Context.Entities.Post, ResGetPost>();
    }
}
