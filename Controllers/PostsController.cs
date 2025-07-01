using BlogsApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context;

    public PostsController(UserManager<IdentityUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var posts = _context.BlogPosts
            .Where(p => p.Status == PostStatus.Approved)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new { p.Id, p.Title, p.Summary, p.AuthorId, p.CreatedAt })
            .ToList();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var post = _context.BlogPosts
            .Where(p => p.Id == id && p.Status == PostStatus.Approved)
            .Select(p => new { p.Id, p.Title, p.Summary, p.Body, p.AuthorId, p.CreatedAt })
            .FirstOrDefault();
        if (post == null)
            return NotFound();
        return Ok(post);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(user);
        var post = new BlogPost
        {
            Title = dto.Title,
            Summary = dto.Summary,
            Body = dto.Body,
            AuthorId = user.Id,
            Status = roles.Contains("Moderator") ? PostStatus.Approved : PostStatus.Pending
        };
        _context.BlogPosts.Add(post);
        await _context.SaveChangesAsync();
        return Ok(post);
    }

    [Authorize(Roles = "Moderator")]
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> Approve(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null)
            return NotFound();
        post.Status = PostStatus.Approved;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [Authorize(Roles = "Moderator")]
    [HttpPut("{id}/reject")]
    public async Task<IActionResult> Reject(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null)
            return NotFound();
        post.Status = PostStatus.Rejected;
        await _context.SaveChangesAsync();
        return Ok();
    }
}