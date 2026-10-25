using System;
using System.Collections.Generic;

//Сервіс аналітики для отримання статистики та звітів про транзакції користувача

namespace FamilyWallet.Core.Interfaces
{
    public interface IAnalyticsService
    {
        decimal GetTotalIncome(DateTime startDate, DateTime endDate);
        decimal GetTotalExpenses(DateTime startDate, DateTime endDate);
        IDictionary<string, decimal> GetExpensesByCategory(DateTime startDate, DateTime endDate);
    }
}