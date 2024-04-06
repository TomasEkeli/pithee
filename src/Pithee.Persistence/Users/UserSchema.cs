namespace Pithee.Persistence.Users;

public class UserSchema : IDefineSchema
{
    public int Version => 1;

    public int Priority => 1;

    public string Name => nameof(UserSchema);

    public string Script =>
    """
        create table if not exists users (
            id text primary key,
            preferred_username text not null,
            password_hash text not null,
            private_key text not null,
            public_key text not null
        )
    """;
}
