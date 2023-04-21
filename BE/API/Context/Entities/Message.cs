using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime CreateAt { get; set; }

    [ForeignKey("Conversation")] public int ConversationId { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }


    public virtual User User { get; set; }

    public virtual Conversation Conversation { get; set; }
}

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
    }
}
