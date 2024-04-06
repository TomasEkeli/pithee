namespace Pithee.Api.ActivityPub;

public record PublicKeyRepresentation(
    string Id,
    string Owner,
    string PublicKeyPem
);