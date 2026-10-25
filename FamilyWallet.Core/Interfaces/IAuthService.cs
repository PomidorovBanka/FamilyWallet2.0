namespace FamilyWallet.Core.Interfaces

//Сервіс аутентифікації та авторизації користувачів

{
    public interface IAuthService
    {
        bool Login(string email, string password);
        bool Register(string username, string email, string password);
        void Logout();
    }
}