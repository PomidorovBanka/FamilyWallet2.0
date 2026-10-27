namespace FamilyWalltet.Test.Services;

public class FamilyManagementServiceTest
{
    [Fact]
    public void CreateFamilyGroup_ValidGroupName_AddsGroup() { }

    [Theory]
    [InlineData("stub")] // TODO: add later
    public void CreateFamilyGroup_InvalidGroupName_ThrowsArgumentException(string groupName) { }

    [Fact]
    public void AddMember_ValidFamilyAndUserIds_AddsMemberToFamily() { }

    [Fact]
    public void AddMember_FamilyNotFound_ThrowsArgumentException() { }

    [Fact]
    public void AddMember_UserNotFound_ThrowsArgumentException() { }

    [Fact]
    public void GetFamilyMembers_ExistingFamilyWithMembers_ReturnsMembers() { }

    [Fact]
    public void GetFamilyMembers_FamilyNotFound_ReturnsEmpty() { }
}