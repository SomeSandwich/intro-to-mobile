namespace API.Types.Objects;

public class MessageRes
{
    public int Id { get; set; }

    public string Content { get; set; }

    public int UserId { get; set; }

    public DateTime CreateAt { get; set; }
}

public class CreateMessageReq
{
    public string Content { get; set; }
}

public class CreateMessageArg
{
    public string Content { get; set; }

    public int ConversationId { get; set; }

    public int UserId { get; set; }
}
