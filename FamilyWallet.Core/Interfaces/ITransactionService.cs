namespace FamilyWallet.Core.Interfaces

// Клас із інформацією про транзакцію

{
    public interface ITransactionService
    {
        void AddTransaction(string category, decimal amount, bool isIncome);
        decimal GetTotalBalance();
    }
}