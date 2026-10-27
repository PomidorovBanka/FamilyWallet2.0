using FamilyWallet.Core.Models;

namespace FamilyWalltet.Test.Services;

public class AnalyticsServiceTest
{
    [Fact]
    public void GetTotalIncome_TransactionsWithinDateRange_ReturnsCorrectSum() { }

    [Fact]
    public void GetTotalIncome_NoTransactionsInRange_ReturnsZero() { }

    [Fact]
    public void GetTotalExpenses_TransactionsWithinDateRange_ReturnsCorrectSum() { }

    [Fact]
    public void GetTotalExpenses_NoTransactionsInRange_ReturnsZero() { }

    [Fact]
    public void GetExpensesByCategory_TransactionsWithinDateRange_ReturnsCorrectGroupedSums() { }

    [Fact]
    public void GetExpensesByCategory_NoTransactionsInRange_ReturnsEmptyDictionary() { }
}