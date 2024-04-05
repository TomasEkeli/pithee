using Microsoft.AspNetCore.Mvc;

namespace Pithee.Api.Signup;

[Route("signup")]
public class SignupController(
    ISignupHandler _handler)
    : ControllerBase
{
    [HttpPost]
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
}