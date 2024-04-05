using Microsoft.AspNetCore.Mvc;

namespace Pithee.Api.Webfinger;

[Route(".well-known/webfinger")]
public class WebfingerController(
    IWebFingerHandler _handler)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<WebFingerResponse>(
        200,
        "application/jrd+json")]
    public async Task<IActionResult> Get(
        [FromQuery] string resource)
    {
        var webfinger = await _handler
            .GetWebfinger(resource);

        Response.ContentType = "application/jrd+json";

        return Ok(webfinger);
    }
}
