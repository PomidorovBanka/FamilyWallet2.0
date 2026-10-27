namespace FamilyWalltet.Test.Services;

public class AuthServiceTest
{
    [Fact]
    public void Register_ValidArguments_ReturnsTrueAndAddsUser() { }

    [Theory]
    [InlineData("stub", "stub", "stub")] // TODO: add later
    public void Register_MissingFields_ReturnsFalse(string username, string email, string password) { }

    [Fact]
    public void Register_DuplicateEmail_ReturnsFalse() { }

    [Fact]
    public void Login_ValidCredentials_ReturnsTrueAndSetsCurrentUser() { }

    [Theory]
    [InlineData("stub", "stub")] // TODO: add later
    public void Login_InvalidCredentials_ReturnsFalse(string email, string password) { }

    [Fact]
    public void Logout_ClearsCurrentUser() { }
}