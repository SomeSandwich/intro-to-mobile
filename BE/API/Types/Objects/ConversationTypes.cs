using System.ComponentModel.DataAnnotations;
using Api.Context.Entities;

namespace API.Types.Objects;

public class CreateConvArg
{
    public int SelfId { get; set; }

    public int UserId { get; set; }
}

public class ConversationRes
{
    public int Id { get; set; }

    public bool IsLock { get; set; }

    public virtual ICollection<ParticipationRes> Participations { get; set; }

    public virtual ICollection<MessageRes> Messages { get; set; }
}

