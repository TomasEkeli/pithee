namespace Pithee.Domain.Users;

public record User(
    string Id,
    string PreferredUsername,
    string PasswordHash,
    string PrivateKey,
    string PublicKey
);
