using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Context.Entities;

public class Participation
{
    [ForeignKey("Conversation")] public int ConversationId { get; set; }
    public Conversation Conversation { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public User User { get; set; }
}

public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
{
    public void Configure(EntityTypeBuilder<Participation> builder)
    {
        builder.HasKey(p => new { p.ConversationId, p.UserId });
    }
}