using FamilyWallet.Core.Models;
using FamilyWallet.Core.Interfaces;

namespace FamilyWallet.Core.Services;

public class FinancialGoalService
{
    private readonly FamilyWalletData _data;
    private readonly ITransactionService _transactionService;

    public FinancialGoalService(
        FamilyWalletData data,
        TransactionService transactionService)
    {
        _data = data;
        _transactionService = transactionService;
    }

    public FinancialGoalModel CreateGoal(
        string title,
        decimal targetAmount,
        DateTime targetDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Вкажіть назву цілі.");

        if (targetAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(targetAmount));

        var goal = new FinancialGoalModel
        {
            Title = title.Trim(),
            TargetAmount = targetAmount,
            CurrentAmount = 0,
            TargetDate = targetDate
        };

        _data.FinancialGoals.Add(goal);
        return goal;
    }

    public void AddSavings(string goalId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        var goal = _data.FinancialGoals.FirstOrDefault(
            g => g.Id == goalId);

        if (goal == null)
            throw new ArgumentException("Фінансова ціль не знайдена.");

        _transactionService.AddTransaction(
            $"Фінансова ціль: {goal.Title}",
            amount,
            false);

        goal.CurrentAmount += amount;
    }

    public IEnumerable<FinancialGoalModel> GetAllGoals()
    {
        return _data.FinancialGoals.ToList();
    }

    public decimal GetProgressPercent(string goalId)
    {
        var goal = _data.FinancialGoals.FirstOrDefault(
            g => g.Id == goalId);

        if (goal == null)
            throw new ArgumentException("Фінансова ціль не знайдена.");

        return Math.Min(
            100m,
            goal.CurrentAmount / goal.TargetAmount * 100m);
    }
}
