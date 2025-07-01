using System.Collections.Generic;
using BlogsApi.Models.Entities;

public enum UserRole
{
    User,
    Moderator,
    Admin
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; }
    public ICollection<Comment> Comments { get; set; }
}