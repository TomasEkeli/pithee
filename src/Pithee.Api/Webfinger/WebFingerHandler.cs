namespace Pithee.Api.Webfinger;

public interface IWebFingerHandler
{
    Task<WebFingerResponse> GetWebfinger(string resource);
}

public class WebFingerHandler : IWebFingerHandler
{
    public Task<WebFingerResponse> GetWebfinger(string resource)
    {
        var webfinger = new WebFingerResponse(
            Subject: resource,
            Links:
            [
                new(
                    Rel: "self",
                    Href: $"/users/{resource}",
                    Type: "application/jrd+json"),
            ]);

        return Task.FromResult(webfinger);
    }
}
