namespace Pithee.Api.Users;

public record User(
    string Username,
    string Password = "password");
