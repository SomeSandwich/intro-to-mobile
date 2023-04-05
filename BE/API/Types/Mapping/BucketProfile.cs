using API.Types.Objects;
using AutoMapper;
using Minio.DataModel;
using Stump.Storage.Types;

namespace API.Types.Mapping;

public class BucketProfile : Profile
{
    public BucketProfile()
    {
        CreateMap<Bucket, ResBucketStat>()
            .ForMember(des => des.CreateTimeInUTC,
                opt => opt.MapFrom(src => src.CreationDate));
    }
}
