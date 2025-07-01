using BlogsApi.Models.DTOs.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/role")]
    public IActionResult UpdateRole(int id, [FromBody] UpdateRoleDto dto)
    {
        // Kullanıcı rolünü güncelle (admin/moderator vs)
        return Ok();
    }
}