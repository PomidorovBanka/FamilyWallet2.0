using FamilyWallet.Core.Interfaces;
using FamilyWallet.Core.Models;

namespace FamilyWallet.Core.Services;

public class FamilyManagementService : IFamilyManagementService
{
    private readonly FamilyWalletData _data;

    public FamilyManagementService(FamilyWalletData data)
    {
        _data = data;
    }

    public void CreateFamilyGroup(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("Назва групи обов'язково.");
        _data.FamilyGroups.Add(new FamilyGroupModel
        {
            Name = groupName.Trim()
        });
    }

    public void AddMember(string familyId, string userId)
    {
        var family = _data.FamilyGroups.FirstOrDefault(
            f => f.Id == familyId);

        var user = _data.Users.FirstOrDefault(
            u => u.Id == userId);

        if (family == null)
            throw new ArgumentException("Сімейна група не знайдена.");

        if (user == null)
            throw new ArgumentException("Користувача не знайдено.");

        if (!family.MemberIds.Contains(userId))
            family.MemberIds.Add(userId);
    }

    public IEnumerable<IUser> GetFamilyMembers(string familyId)
    {
        var family = _data.FamilyGroups.FirstOrDefault(
            f => f.Id == familyId);

        if (family == null)
            return Enumerable.Empty<IUser>();

        return _data.Users
            .Where(u => family.MemberIds.Contains(u.Id))
            .Cast<IUser>()
            .ToList();
    }
}
