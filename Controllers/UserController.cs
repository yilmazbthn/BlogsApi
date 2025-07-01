using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogsApi.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(UserManager<IdentityUser> userManager):ControllerBase
{
 [HttpGet("")]

 [Authorize]
 public IActionResult Index()
 {
  var userId = userManager.GetUserId(User);
  return Ok();
 }
}