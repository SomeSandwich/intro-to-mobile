using Newtonsoft.Json;

namespace API.Types.Objects;

public class CreateCommentReq
{
    public int UserId { get; set; }

    public int PostId { get; set; }

    public string Content { get; set; }

    [JsonIgnore] public DateTime CreateAt { get; set; }
}

public class CommentRes
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public string Content { get; set; }

    public DateTime CreateAt { get; set; }
}
