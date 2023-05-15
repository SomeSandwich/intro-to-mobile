using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostReq, Post>();
        // On server timezone is in UTC so add 7 hours
        // to move timezone to Vietname
        CreateMap<Post, PostRes>()
            .ForMember(des => des.CreatedDate,
                opt => opt.MapFrom(src => src.CreatedDate.AddHours(7)))
            .ForMember(des => des.UpdatedDate,
                opt => opt.MapFrom(src => src.UpdatedDate.AddHours(7)));
    }
}
