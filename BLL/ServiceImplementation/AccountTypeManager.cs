using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    public class AccountTypeManager : IAccountTypeManager
    {
        private readonly IAccountStorage _storage;

        public AccountTypeManager(IAccountStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<AccountType> GetAccountTypes()
        {
            var types = _storage.GetAccountTypes();
            return Mappers.Mapper.MapTypes(types);
        }
    }
}
