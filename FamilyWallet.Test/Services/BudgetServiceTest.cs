using System.Runtime.InteropServices;
using FamilyWallet.Core.Models;

namespace FamilyWalltet.Test;

public class BudgetServiceTest
{
    [Fact]
    public void SetLimit_ValidCategoryAndAmount_AddsNewLimit() { }

    [Fact]
    public void SetLimit_ExistingCategory_UpdatesExistingLimit() { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void SetLimit_InvalidCategory_ThrowsArgumentException(string category) { }

    [Theory]
    [InlineData(0)] // TODO: add later
    public void SetLimit_InvalidMaxAmount_ThrowsArgumentException(decimal maxAmount) { }

    [Fact]
    public void GetSpentAmount_CategoryWithTransactions_ReturnsCorrectSumForCurrentMonth() { }

    [Fact]
    public void GetSpentAmount_CategoryWithoutTransactions_ReturnsZero() { }

    [Fact]
    public void IsLimitExceeded_LimitExceeded_ReturnsTrue() { }

    [Fact]
    public void IsLimitExceeded_LimitNotExceeded_ReturnsFalse() { }

    [Fact]
    public void IsLimitExceeded_LimitNotFound_ReturnsFalse() { }

    [Fact]
    public void GetAllLimits_UpdatesSpentAmountsAndReturnsAllLimits() { }
}