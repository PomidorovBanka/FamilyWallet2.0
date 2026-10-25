using System;

// Клас із інформацією про фінансову ціль користувача

namespace FamilyWallet.Core.Models
{
    public class FinancialGoalModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Title { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime TargetDate { get; set; }
    }
}