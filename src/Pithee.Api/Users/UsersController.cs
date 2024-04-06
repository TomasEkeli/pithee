using Microsoft.AspNetCore.Mvc;
using Pithee.Api.Users.Signup;

namespace Pithee.Api.Users;

[Route("/users")]
public class UsersController(IUsersHandler _handler) : ControllerBase
{
    [HttpPost("signup")]
    [ProducesResponseType<SignupResponse>(
        201,
        "application/json")]
    public async Task<IActionResult> Post(
        [FromBody] SignupRequest request)
    {
        var response = await _handler
            .Signup(request);

        return Created(
            $"/users/{response.Username}",
            response);
    }


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