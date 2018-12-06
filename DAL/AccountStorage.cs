using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return _db.BankAccounts.Find(number);
        }

        public IEnumerable<BankAccountDTO> GetAccounts()
        {
            return _db.BankAccounts.Include(t => t.Owner).Include(t => t.AccountType);
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
