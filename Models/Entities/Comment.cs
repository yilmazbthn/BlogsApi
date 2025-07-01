using System;
using Microsoft.AspNetCore.Identity;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public string AuthorId { get; set; }
    public IdentityUser Author { get; set; }
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }

    public string Reports { get; set; }
}