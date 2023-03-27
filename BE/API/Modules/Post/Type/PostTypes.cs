using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Post.Type;

#region Post

public class ReqCreatePost
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int Price { get; set; }
 
    public string Caption { get; set; }
    
    public string Description { get; set; }
    
    public IFormFileCollection MediaFiles { get; set; }
}

#endregion