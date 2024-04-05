using Microsoft.AspNetCore.Mvc;

namespace Pithee.Api.Webfinger;

[Route(".well-known/webfinger")]
public class WebfingerController(
    IWebFingerHandler handler)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<WebFingerResponse>(
        200,
        "application/jrd+json")]
    public async Task<IActionResult> Get(
        [FromQuery] string resource)
    {
        var webfinger = await handler
            .GetWebfinger(resource);

        Response.ContentType = "application/jrd+json";

        return Ok(webfinger);
    }
}
