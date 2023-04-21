using System.Runtime.Serialization;
using Api.Context;
using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IConversationService
{
    Task<ConversationRes?> GetAsync(int id);
    Task<IEnumerable<ConversationRes>> GetByUserIdAsync(int userId);

    Task<int> FindConversation(int userId1, int userId2);
    Task<int> AddAsync(CreateConvArg req);
}

public class ConverstationService : IConversationService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;

    public ConverstationService(MobileDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ConversationRes>> GetByUserIdAsync(int userId)
    {
        var convs = await _context.Conversations
            .Include(e => e.Participations)
            .Where(e => e.Participations.Any(e => e.UserId == userId))
            .ToListAsync();

        return _mapper.Map<List<Conversation>, IEnumerable<ConversationRes>>(convs);
    }

    public async Task<int> FindConversation(int userId1, int userId2)
    {
        var exist = from p1 in _context.Set<Participation>()
            join p2 in _context.Set<Participation>()
                on p1.ConversationId equals p2.ConversationId
            where p1.UserId == userId1 && p2.UserId == userId2
            select p1.ConversationId;

        int any = await exist.FirstOrDefaultAsync();

        return any;
    }

    public async Task<int> AddAsync(CreateConvArg req)
    {
        try
        {
            var checkExists = await FindConversation(req.SelfId, req.UserId);
            if (checkExists != 0)
                return checkExists;

            var conv = new Conversation();

            await _context.Conversations.AddAsync(conv);
            await _context.SaveChangesAsync();

            List<Participation> listPart = new()
            {
                new Participation { ConversationId = conv.Id, UserId = req.SelfId },
                new Participation { ConversationId = conv.Id, UserId = req.UserId }
            };
            await _context.Participations.AddRangeAsync(listPart);
            await _context.SaveChangesAsync();

            return conv.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ConversationRes?> GetAsync(int id)
    {
        var conv = await _context.Conversations
            .Include(e => e.Participations)
            .Include(e => e.Messages.OrderByDescending(m => m.CreateAt))
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = conv is null ? null : _mapper.Map<Conversation, ConversationRes>(conv);

        return result;
    }
}
