using FamilyWallet.Core.Models;

namespace FamilyWallet.Core.Services;

public class BudgetService
{
    private readonly FamilyWalletData _data;

    public BudgetService(FamilyWalletData data)
    {
        _data = data;
    }
    
    public BudgetLimitModel SetLimit(string category, decimal maxAmount)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Категорія не може бути порожньою.");

        if (maxAmount <= 0)
            throw new ArgumentException("Ліміт має бути більшим за нуль.");

        category = category.Trim();

        var existingLimit = _data.BudgetLimits.FirstOrDefault(
            x => string.Equals(
                x.CategoryId,
                category,
                StringComparison.OrdinalIgnoreCase));

        if (existingLimit != null)
        {
            existingLimit.MaxAmount = maxAmount;
            UpdateSpentAmount(existingLimit);
            return existingLimit;
        }

        var limit = new BudgetLimitModel
        {
            CategoryId = category,
            MaxAmount = maxAmount,
            CurrentAmount = GetSpentAmount(category)
        };

        _data.BudgetLimits.Add(limit);
        return limit;
    }


    public decimal GetSpentAmount(string category)
    {
        var now = DateTime.Now;

        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        var startOfNextMonth = startOfMonth.AddMonths(1);

        return _data.Transactions
            .Where(t =>
                !t.IsIncome &&
                t.Category.Equals(
                    category,
                    StringComparison.OrdinalIgnoreCase) &&
                t.Date >= startOfMonth &&
                t.Date < startOfNextMonth)
            .Sum(t => t.Amount);
    }

    public bool IsLimitExceeded(string category)
    {
        var limit = _data.BudgetLimits.FirstOrDefault(b =>
            string.Equals(b.CategoryId, category,
                StringComparison.OrdinalIgnoreCase));

        if (limit == null)
            return false;

        UpdateSpentAmount(limit);
        return limit.CurrentAmount > limit.MaxAmount;
    }

    public IEnumerable<BudgetLimitModel> GetAllLimits()
    {
        foreach (var limit in _data.BudgetLimits)
            UpdateSpentAmount(limit);

        return _data.BudgetLimits.ToList();
    }

    private void UpdateSpentAmount(BudgetLimitModel limit)
    {
        limit.CurrentAmount = GetSpentAmount(limit.CategoryId);
    }
}
