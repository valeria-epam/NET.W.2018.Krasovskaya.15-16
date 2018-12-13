using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;

namespace DAL.Fake.Repositories
{
    /// <summary>
    /// Class for storing account information.
    /// </summary>
    public class AccountStorage : IAccountStorage
    {
        private readonly string _path;
        private IList<BankAccountDTO> _accounts;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountStorage"/>. 
        /// </summary>
        public AccountStorage(string path)
        {
            _path = path;
        }

        private IList<BankAccountDTO> Accounts
        {
            get
            {
                if (_accounts == null)
                {
                    LoadAccounts();
                }

                return _accounts;
            }
        }

        /// <summary>
        /// Checks if the account exists.
        /// </summary>
        public bool AccountExists(BankAccountDTO account)
        {
            return Accounts.Contains(account);
        }

        /// <summary>
        /// Adds the account to the storage.
        /// </summary>
        public void AddAccount(BankAccountDTO account)
        {
            Accounts.Add(account);
        }

        /// <summary>
        /// Delete the account with the specified <paramref name="number"/> from the storage.
        /// </summary>
        public void DeleteAccount(string number)
        {
            Accounts.Remove(_accounts.First(t => t.Number == number));
        }

        /// <summary>
        /// Gets the account with the specified <paramref name="number"/>.
        /// </summary>
        public BankAccountDTO GetAccount(string number)
        {
            return Accounts.FirstOrDefault(t => t.Number == number);
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        public IEnumerable<BankAccountDTO> GetAccounts()
        {
            return Accounts;
        }

        public IEnumerable<AccountTypeDTO> GetAccountTypes()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves the accounts to the storage.
        /// </summary>
        public void Save()
        {
            WriteAccounts(Accounts);
        }

        /// <summary>
        /// Loads the accounts from the file.
        /// </summary>
        public void LoadAccounts()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(_path, FileMode.OpenOrCreate)))
            {
                _accounts = new List<BankAccountDTO>();

                while (reader.PeekChar() > -1)
                {
                    string number = reader.ReadString();
                    string name = reader.ReadString();
                    string surname = reader.ReadString();
                    decimal sum = reader.ReadDecimal();
                    decimal bonus = reader.ReadDecimal();
                    string typeName = reader.ReadString();
                    int balanceCost = reader.ReadInt32();
                    int refillCost = reader.ReadInt32();
                    AccountStateDTO state = (AccountStateDTO)reader.ReadInt32();

                    var account = new BankAccountDTO()
                    {
                        Number = number,
                        AccountType = new AccountTypeDTO()
                        {
                            TypeName = typeName,
                            RefillCost = refillCost,
                            BalanceCost = balanceCost
                        },
                        Owner = new AccountOwnerDTO()
                        {
                            Name = name,
                            Surname = surname
                        },
                        Sum = sum,
                        Bonus = bonus,
                        State = state
                    };

                    _accounts.Add(account);
                }
            }
        }

        private static void WriteAccount(BankAccountDTO account, BinaryWriter writer)
        {
            writer.Write(account.Number);
            writer.Write(account.Owner.Name);
            writer.Write(account.Owner.Surname);
            writer.Write(account.Sum);
            writer.Write(account.Bonus);
            writer.Write(account.AccountType.TypeName);
            writer.Write(account.AccountType.BalanceCost);
            writer.Write(account.AccountType.RefillCost);
            writer.Write((int)account.State);
        }

        private void WriteAccounts(IEnumerable<BankAccountDTO> bankAccounts)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.Create)))
            {
                foreach (var account in bankAccounts)
                {
                    WriteAccount(account, writer);
                }
            }
        }
    }
}
