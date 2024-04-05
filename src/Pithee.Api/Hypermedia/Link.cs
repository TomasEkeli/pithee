namespace Pithee.Api.Hypermedia;

public record Link(
    string Rel,
    string Href,
    string Type);