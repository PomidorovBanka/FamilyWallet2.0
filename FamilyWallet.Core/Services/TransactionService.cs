using System;
using System.Collections.Generic;
using System.Linq;
using FamilyWallet.Core.Interfaces;
using FamilyWallet.Core.Models;

namespace FamilyWallet.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly List<TransactionModel> _transactions;

        public TransactionService(FamilyWalletData data)
        {
            _transactions = data.Transactions;
        }

        public IReadOnlyList<TransactionModel> Transactions => _transactions;

        public void AddTransaction(
            string category,
            decimal amount,
            bool isIncome)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Категорія не може бути порожньою.");

            if (amount <= 0)
                throw new ArgumentException("Сума має бути більшою за нуль.");

            var transaction = new TransactionModel
            {
                Category = category.Trim(),
                Amount = amount,
                IsIncome = isIncome,
                Date = DateTime.Now
            };

            _transactions.Add(transaction);
        }

        public decimal GetTotalBalance()
        {
            return _transactions.Sum(t =>
                t.IsIncome ? t.Amount : -t.Amount);
        }
    }
}
