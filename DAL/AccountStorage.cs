using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;

namespace DAL
{
    public class AccountStorage : IAccountStorage
    {
        private readonly ApplicationContext _db;

        public AccountStorage(ApplicationContext db)
        {
            _db = db;
        }

        public bool AccountExists(BankAccountDTO account)
        {
            return _db.BankAccounts.Any(x => x.Number == account.Number);
        }

        public void AddAccount(BankAccountDTO account)
        {
            account.AccountType = _db.AccountTypes.Find(account.AccountType.TypeName);
            _db.BankAccounts.Add(account);
        }

        public void DeleteAccount(string number)
        {
            var account = _db.BankAccounts.Find(number);
            if (account != null)
            {
                _db.BankAccounts.Remove(account);
            }
        }

        public BankAccountDTO GetAccount(string number)
        {
            return _db.BankAccounts.Include(dto => dto.Owner).Include(dto => dto.AccountType).FirstOrDefault(dto => dto.Number == number);
        }

        public IEnumerable<BankAccountDTO> GetAccounts()
        {
            return _db.BankAccounts.Include(t => t.Owner).Include(t => t.AccountType);
        }

        public IEnumerable<AccountTypeDTO> GetAccountTypes()
        {
            return _db.AccountTypes;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void LoadAccounts()
        {
        }
    }
}
