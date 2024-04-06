using Microsoft.AspNetCore.Mvc;

namespace Pithee.Api.Users;

[Route("/users")]
public class UsersController(IUsersHandler _handler) : ControllerBase
{
    [HttpGet("{username}")]
    public async Task<IActionResult> GetUser(
        string username)
    {
        var user = await _handler.GetUser(username);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(new UserResponse(user.Username));
    }

}