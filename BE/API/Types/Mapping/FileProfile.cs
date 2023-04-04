using API.Types.Objects;
using AutoMapper;
using Minio.DataModel;
using Item = Minio.DataModel.Item;

namespace API.Types.Mapping;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<Item, ResStatFile>();
        CreateMap<ObjectStat, ResDetailStatFile>()
            .ForMember(des => des.Key,
                opt => opt.MapFrom(src => src.ObjectName));
    }
}
