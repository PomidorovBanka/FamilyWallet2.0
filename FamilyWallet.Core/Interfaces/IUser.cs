namespace FamilyWallet.Core.Interfaces

//Клас із інформацією про користувача

{
    public interface IUser
    {
        string Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Role { get; set; }
    }
}