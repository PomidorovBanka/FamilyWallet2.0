using System;
using FamilyWallet.Core.Interfaces;

namespace FamilyWallet.Core.Models
{
    public class UserModel : IUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Member"; // Admin, Member, Child
        public string PasswordHash { get; set; } = string.Empty;
    }
}