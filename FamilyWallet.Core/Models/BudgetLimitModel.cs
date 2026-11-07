namespace FamilyWallet.Core.Models
{

    // Клас із інформацією про ліміт бюджету для певної категорії
    public class BudgetLimitModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string CategoryId { get; set; } = string.Empty;
        public decimal MaxAmount { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}