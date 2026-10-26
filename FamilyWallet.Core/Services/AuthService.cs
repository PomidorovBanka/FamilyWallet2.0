using System;
using System.Security.Cryptography;
using FamilyWallet.Core.Interfaces;
using FamilyWallet.Core.Models;

namespace FamilyWallet.Core.Services;

public class AuthService : IAuthService
{
    private readonly FamilyWalletData _data;
    private UserModel? _currentUser;

    public AuthService(FamilyWalletData data)
    {
        _data = data;
    }

    public bool Register(
        string username,
        string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
            return false;

        if (_data.Users.Any(u =>
            string.Equals(u.Email, email.Trim(),
                StringComparison.OrdinalIgnoreCase)))
            return false;

        var user = new UserModel
        {
            Username = username.Trim(),
            Email = email.Trim(),
            Role = "Member",
            PasswordHash = HashPassword(password)
        };

        _data.Users.Add(user);
            
        return true;
    }

    public bool Login(string email, string password)
    {
        var user = _data.Users.FirstOrDefault(u =>
            string.Equals(u.Email, email.Trim(),
                StringComparison.OrdinalIgnoreCase));

        if (user == null ||
            !VerifyPassword(password, user.PasswordHash))
            return false;

        _currentUser = user;
        return true;
    }

    public void Logout()
    {
        _currentUser = null;
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256,
            32);

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(
        string password, string storedHash)
    {
        string[] parts = storedHash.Split(':');

        if (parts.Length != 2)
            return false;

        try
        {
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                100_000,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(
                actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
