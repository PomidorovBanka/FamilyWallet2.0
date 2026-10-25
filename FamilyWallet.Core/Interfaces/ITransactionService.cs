namespace FamilyWallet.Core.Interfaces
{
    public interface ITransactionService
    {
        void AddTransaction(string category, decimal amount, bool isIncome);
        decimal GetTotalBalance();
    }
}