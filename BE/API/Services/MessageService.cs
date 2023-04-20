using Api.Context;
using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;

namespace API.Services;

public interface IMessageService
{
    Task<int> AddAsync(CreateMessageArg arg);
}

public class MessageService : IMessageService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;

    public MessageService(MobileDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(CreateMessageArg arg)
    {
        var mes = _mapper.Map<CreateMessageArg, Message>(arg);

        await _context.Messages.AddAsync(mes);
        await _context.SaveChangesAsync();

        return mes.Id;
    }
}
