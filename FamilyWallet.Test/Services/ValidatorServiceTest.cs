namespace FamilyWalltet.Test.Services;

public class ValidatorServiceTest
{
    [Theory]
    [InlineData("stub")] // TODO: add later
    public void IsValidUsername_ValidUsernames_ReturnsTrue(string username) { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void IsValidUsername_InvalidUsernames_ReturnsFalse(string username) { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void IsValidEmail_ValidEmails_ReturnsTrue(string email) { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void IsValidEmail_InvalidEmails_ReturnsFalse(string email) { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void IsValidPassword_ValidPasswords_ReturnsTrue(string password) { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void IsValidPassword_InvalidPasswords_ReturnsFalse(string password) { }
}