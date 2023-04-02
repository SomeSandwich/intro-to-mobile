using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Api.Context.Entities.Category, CategoryRes>();

        CreateMap<CreateCategoryReq, Api.Context.Entities.Category>();
    }
}
