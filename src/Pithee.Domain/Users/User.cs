using Pithee.CodeGen;

namespace Pithee.Domain.Users;

[GenerateCtor]
public partial record User(
    string Id,
    string PreferredUsername,
    string PasswordHash,
    string PrivateKey,
    string PublicKey
);
