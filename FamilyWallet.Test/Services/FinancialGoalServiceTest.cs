namespace FamilyWalltet.Test.Services;

public class FinancialGoalServiceTest
{
    [Fact]
    public void CreateGoal_ValidArguments_AddsGoalAndReturnsIt() { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void CreateGoal_InvalidTitle_ThrowsArgumentException(string title) { }

    [Theory]
    [InlineData(0)] // TODO: add later
    public void CreateGoal_InvalidTargetAmount_ThrowsArgumentOutOfRangeException(decimal targetAmount) { }

    [Fact]
    public void AddSavings_ValidAmountAndExistingGoal_UpdatesAmountAndAddsTransaction() { }

    [Theory]
    [InlineData(0)] // TODO: add later
    public void AddSavings_InvalidAmount_ThrowsArgumentOutOfRangeException(decimal amount) { }

    [Fact]
    public void AddSavings_GoalNotFound_ThrowsArgumentException() { }

    [Fact]
    public void GetAllGoals_ReturnsAllExistingGoals() { }

    [Fact]
    public void GetProgressPercent_ExistingGoal_ReturnsCorrectPercentage() { }

    [Fact]
    public void GetProgressPercent_ExceedsTarget_CappedAt100Percent() { }

    [Fact]
    public void GetProgressPercent_GoalNotFound_ThrowsArgumentException() { }
}