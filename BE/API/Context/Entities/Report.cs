using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Context.Entities;

public class Report
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Reason { get; set; }

    public DateTime ReportAt { get; set; }


    [ForeignKey("Post")] public int PostId { get; set; }
    public virtual Post Post { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public virtual User User { get; set; }
}