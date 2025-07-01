using System.ComponentModel.DataAnnotations;

namespace BlogsApi.Models.Entities;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Yorum hangi posta ait?
    public int BlogPostId { get; set; }
    public BlogPost? BlogPost { get; set; }
    
    public string UserId { get; set; }
    public User? User { get; set; }

    // Şikayetli mi?
    public bool IsReported { get; set; } = false;
}
