using System;

namespace FamilyWallet.Core.Models
{
    public class TransactionModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; } // true — дохід, false — витрата
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}