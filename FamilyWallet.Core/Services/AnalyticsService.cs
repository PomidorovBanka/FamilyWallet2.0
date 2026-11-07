using FamilyWallet.Core.Models;

namespace FamilyWallet.Core.Services;

public class AnalyticsService
{
    private readonly List<TransactionModel> _transactions;

    public AnalyticsService(List<TransactionModel> transactions)
    {
        _transactions = transactions;
    }

    public decimal GetTotalIncome(DateTime startDate, DateTime endDate)
    {
        return _transactions
            .Where(t => t.IsIncome &&
                        t.Date >= startDate &&
                        t.Date < endDate)
            .Sum(t => t.Amount);
    }

    public decimal GetTotalExpenses(DateTime startDate, DateTime endDate)
    {
        return _transactions
            .Where(t => !t.IsIncome &&
                        t.Date >= startDate &&
                        t.Date < endDate)
            .Sum(t => t.Amount);
    }

    public Dictionary<string, decimal> GetExpensesByCategory(
        DateTime startDate,
        DateTime endDate)
    {
        return _transactions
            .Where(t => !t.IsIncome &&
                        t.Date >= startDate &&
                        t.Date < endDate)
            .GroupBy(t => t.Category)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(t => t.Amount));
    }
}
