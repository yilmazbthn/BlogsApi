using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public enum PostStatus
{
    Pending,
    Approved,
    Rejected
}

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Body { get; set; }
    public string AuthorId { get; set; }
    public IdentityUser Author { get; set; }
    public PostStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Comment> Comments { get; set; }
}