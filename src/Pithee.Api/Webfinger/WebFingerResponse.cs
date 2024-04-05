using Pithee.Api.Hypermedia;

namespace Pithee.Api.Webfinger;

public record WebFingerResponse(
    string Subject,
    string[]? Aliases = null,
    Link[]? Links = null);
