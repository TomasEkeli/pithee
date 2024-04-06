namespace Pithee.Domain.Users;

public record User(
    string Username,
    string Password = "password");
