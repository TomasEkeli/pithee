using Pithee.Api.ActivityPub;

namespace Pithee.Api.Users;

public record UserResponse(
    string Id,
    string PreferredUsername,
    PublicKeyRepresentation PublicKey
) : JsonLdDocument(
    Context: "https://www.w3.org/ns/activitystreams",
    Id: Id,
    Type: "Person"
);
