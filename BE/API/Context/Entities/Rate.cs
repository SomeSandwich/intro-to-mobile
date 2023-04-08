using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Api.Context.GenerateData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Context.Entities;

public class Rate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime RatingAt { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey("Post")] public int PostId { get; set; }
    public virtual Post Post { get; set; }
}

public class RateDetailConfiguration : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        builder.HasData(FakerGenerating.Rates);
    }
}
