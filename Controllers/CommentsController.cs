using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using BlogsApi.Data;

[ApiController]
[Route("comments")]
public class CommentsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context;

    public CommentsController(UserManager<IdentityUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    [Authorize(Roles = "Moderator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
            return NotFound();

        comment.IsDeleted = true;
        await _context.SaveChangesAsync();
        return Ok("Yorum silindi (soft delete).");
    }

    // Yorum Şikayet Etme (Giriş yapmış kullanıcılar)
    [Authorize]
    [HttpPost("{id}/report")]
    public async Task<IActionResult> Report(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
            return NotFound();

        if (comment.AuthorId == user.Id)
            return BadRequest("Kendi yorumunu şikayet edemezsin.");

        var reportedUserIds = (comment.Reports ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (reportedUserIds.Contains(user.Id))
            return BadRequest("Bu yorumu zaten şikayet ettiniz.");

        reportedUserIds.Add(user.Id);
        comment.Reports = string.Join(",", reportedUserIds);

        await _context.SaveChangesAsync();

        // Örnek: Eğer şikayet sayısı 3'ü geçerse otomatik olarak moderatöre bildirim/işaretleme yapılabilir
        // if (reportedUserIds.Count >= 3) { ... }

        return Ok("Yorum şikayet edildi.");
    }
}