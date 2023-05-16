using System.Security.Cryptography;
using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<CreateMessageReq, CreateMessageArg>();
        CreateMap<CreateMessageArg, Message>()
            .AfterMap((src, des) => des.CreateAt = DateTime.Now.ToUniversalTime());

        CreateMap<Message, MessageRes>();
    }
}
