using FamilyWallet.Core.Models;

namespace FamilyWallet.Core.Services;

public class FamilyWalletData
{
    public List<UserModel> Users { get; } = new();
    public List<FamilyGroupModel> FamilyGroups { get; } = new();
    public List<TransactionModel> Transactions { get; } = new();
    public List<BudgetLimitModel> BudgetLimits { get; } = new();
    public List<FinancialGoalModel> FinancialGoals { get; } = new();
}
