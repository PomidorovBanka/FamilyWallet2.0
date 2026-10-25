using System.Collections.Generic;

//Управління додаванням та видаленням членів сім'ї

namespace FamilyWallet.Core.Interfaces
{
    public interface IFamilyManagementService
    {
        void CreateFamilyGroup(string groupName);
        void AddMember(string familyId, string userId);
        IEnumerable<IUser> GetFamilyMembers(string familyId);
    }
}