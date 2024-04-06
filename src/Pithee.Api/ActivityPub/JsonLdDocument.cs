namespace Pithee.Api.ActivityPub;

public abstract record JsonLdDocument(
    string Context,
    string Id,
    string Type
);


