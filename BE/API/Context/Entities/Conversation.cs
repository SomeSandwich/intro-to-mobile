using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Context.Entities;

public class Conversation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool IsLock { get; set; } = false;
    
    public virtual ICollection<Participation> Participations { get; set; }
    
    public virtual ICollection<Message> Messages { get; set; }
}