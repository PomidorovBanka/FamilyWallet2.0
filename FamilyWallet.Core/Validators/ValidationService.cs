using System.Text.RegularExpressions;

namespace FamilyWallet.Core.Validators;

public class ValidationService
{
    public bool IsValidUsername(string username)
    {
        return !string.IsNullOrWhiteSpace(username);
    }

    public bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) &&
               Regex.IsMatch(
                   email,
                   @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool IsValidPassword(string password)
    {
        return !string.IsNullOrWhiteSpace(password) &&
               password.Length >= 8 &&
               Regex.IsMatch(password, @"[A-ZА-ЯІЇЄҐ]") &&
               Regex.IsMatch(password, @"\d");
    }
}