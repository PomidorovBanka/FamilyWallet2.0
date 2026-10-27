namespace FamilyWalltet.Test.Services;

public class TransactionServiceTest
{
    [Fact]
    public void AddTransaction_ValidArguments_AddsTransactionToList() { }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddTransaction_InvalidCategory_ThrowsArgumentException(string category) { }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void AddTransaction_InvalidAmount_ThrowsArgumentException(decimal amount) { }

    [Fact]
    public void GetTotalBalance_EmptyTransactions_ReturnsZero() { }

    [Fact]
    public void GetTotalBalance_MixedTransactions_ReturnsCorrectBalance() { }

    [Fact]
    public void Transactions_ReturnsReadOnlyListOfTransactions() { }
}