using Api.Context.Entities;
using API.Services;
using API.Types.Objects;
using AutoMapper;

namespace API.Types.Mapping;

public class ConversationProfile : Profile
{
    public ConversationProfile()
    {
        CreateMap<Participation, ParticipationRes>();
        CreateMap<Conversation, ConversationRes>();
    }
}
