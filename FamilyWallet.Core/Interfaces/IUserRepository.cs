using System.Collections.Generic;

namespace FamilyWallet.Core.Interfaces
{
    public interface IUserRepository
    {
        IUser GetById(string id);
        IUser GetByEmail(string email);
        void Save(IUser user);
    }
}