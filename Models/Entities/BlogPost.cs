using System.ComponentModel.DataAnnotations;

namespace BlogsApi.Models.Entities;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PostStatus Status { get; set; } = PostStatus.Pending;
    
    public string UserId { get; set; }
    public User? User { get; set; }

    public List<Comment>? Comments { get; set; }
}

public enum PostStatus
{
    Pending,
    Approved,
    Rejected
}
