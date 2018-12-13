using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    public interface IAccountTypeManager
    {
        IEnumerable<AccountType> GetAccountTypes();
    }
}
