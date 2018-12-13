using System;
using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Class for managing account information.
    /// </summary>
    public class AccountManager : IAccountManager
    {
        private readonly ICalculateBonus _calculateBonus;
        private readonly IAccountStorage _storage;
        private readonly IGenerateNumber _generateNumber;
        private readonly INotificationService _service;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountManager"/>. 
        /// </summary>
        public AccountManager(ICalculateBonus calculateBonus, IAccountStorage storage, IGenerateNumber generateNumber, INotificationService service)
        {
            _calculateBonus = calculateBonus;
            _storage = storage;
            _generateNumber = generateNumber;
            _service = service;
        }

        /// <summary>
        /// Adds a new account if it doesn't exist.
        /// </summary>
        public void AddBankAccount(BankAccount account)
        {
            CheckAccount(account);
            account.Number = _generateNumber.Generate();
            bool hasAccount = _storage.AccountExists(Mappers.Mapper.MapAccountToDTO(account));
            if (!hasAccount)
            {
                _storage.AddAccount(Mappers.Mapper.MapAccountToDTO(account));
                _storage.Save();
            }
            else
            {
                throw new Exception("Our storage already has this account.");
            }
        }

        /// <summary>
        /// Transfers amount of money from one account to another account.
        /// </summary>
        public void Transfer(BankAccount accountFrom, BankAccount accountTo, decimal amountOfMoney)
        {
            if (amountOfMoney <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfMoney));
            }

            CheckAccount(accountFrom);
            CheckAccount(accountTo);

            var bankAccountFrom = _storage.GetAccount(accountFrom.Number);
            var bankAccountTo = _storage.GetAccount(accountTo.Number);
            if (bankAccountFrom == null || bankAccountTo == null)
            {
                throw new Exception("Our storage doesn't have this account.");
            }

            if (bankAccountFrom.State == AccountStateDTO.Closed || bankAccountTo.State == AccountStateDTO.Closed)
            {
                throw new Exception("Your account is closed.");
            }

            if (bankAccountFrom.Sum < amountOfMoney)
            {
                throw new Exception("You don't have enough money on your account.");
            }

            bankAccountFrom.Sum -= amountOfMoney;
            bankAccountTo.Sum += amountOfMoney;
            var bonusCountFrom = _calculateBonus.RefillBonus(Mappers.Mapper.MapAccount(bankAccountFrom), amountOfMoney);
            bankAccountFrom.Bonus -= bonusCountFrom;
            if (bankAccountFrom.Bonus < 0m)
            {
                bankAccountFrom.Bonus = 0m;
            }

            var bonusCountTo = _calculateBonus.RefillBonus(Mappers.Mapper.MapAccount(bankAccountTo), amountOfMoney);
            bankAccountTo.Bonus += bonusCountTo;
            accountFrom.Sum = bankAccountFrom.Sum;
            accountTo.Sum = bankAccountTo.Sum;
            accountFrom.Bonus = bankAccountFrom.Bonus;
            accountTo.Bonus = bankAccountTo.Bonus;
            _storage.Save();
            string bodyTo = $"{amountOfMoney} credited to your account. Accrued {bonusCountTo} bonus points.";
            string bodyFrom = $"{amountOfMoney} withdrawn from your account. {bonusCountFrom} bonus points shot.";
            _service.Send(accountTo.Email, bodyTo);
            _service.Send(accountFrom.Email, bodyFrom);
        }

        /// <summary>
        /// Close account if it exists.
        /// </summary>
        public void CloseBankAccount(BankAccount account)
        {
            CheckAccount(account);
            var bankAccount = _storage.GetAccount(account.Number);
            if (bankAccount != null)
            {
                bankAccount.State = AccountStateDTO.Closed;
                _storage.Save();
            }
            else
            {
                throw new Exception("Our storage doesn't have this account.");
            }
        }

        /// <summary>
        /// Adds amount of money to the account.
        /// </summary>
        public void RefillAccount(BankAccount account, decimal amountOfMoney)
        {
            if (amountOfMoney <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfMoney));
            }

            CheckAccount(account);

            var bankAccount = _storage.GetAccount(account.Number);
            if (bankAccount == null)
            {
                throw new Exception("Our storage doesn't have this account.");
            }

            if (bankAccount.State == AccountStateDTO.Closed)
            {
                throw new Exception("Your account is closed.");
            }

            bankAccount.Sum += amountOfMoney;
            var bonusCount = _calculateBonus.RefillBonus(Mappers.Mapper.MapAccount(bankAccount), amountOfMoney);
            bankAccount.Bonus += bonusCount;
            account.Sum = bankAccount.Sum;
            account.Bonus = bankAccount.Bonus;
            _storage.Save();
            string body = $"{amountOfMoney} credited to your account. Accrued {bonusCount} bonus points.";
            _service.Send(account.Email, body);
        }

        /// <summary>
        /// Withdraws amount of money from account.
        /// </summary>
        public void WithdrawalAccount(BankAccount account, decimal amountOfMoney)
        {
            if (amountOfMoney <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfMoney));
            }

            CheckAccount(account);

            var bankAccount = _storage.GetAccount(account.Number);
            if (bankAccount == null)
            {
                throw new Exception("Our storage doesn't have this account.");
            }

            if (bankAccount.State == AccountStateDTO.Closed)
            {
                throw new Exception("Your account is closed.");
            }

            if (bankAccount.Sum < amountOfMoney)
            {
                throw new Exception("You don't have enough money on your account.");
            }

            bankAccount.Sum -= amountOfMoney;
            var bonusCount = _calculateBonus.WithdrawalBonus(Mappers.Mapper.MapAccount(bankAccount), amountOfMoney);
            bankAccount.Bonus -= bonusCount;
            if (bankAccount.Bonus < 0m)
            {
                bankAccount.Bonus = 0m;
            }

            account.Sum = bankAccount.Sum;
            account.Bonus = bankAccount.Bonus;
            _storage.Save();
            string body = $"{amountOfMoney} withdrawn from your account. {bonusCount} bonus points shot.";
            _service.Send(account.Email, body);
        }

        /// <summary>
        /// Gets account by account <paramref name="number"/>.
        /// </summary>
        public BankAccount GetAccount(string number)
        {
            var account = _storage.GetAccount(number);
            return Mappers.Mapper.MapAccount(account);
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        public IEnumerable<BankAccount> GetAccounts()
        {
            var accounts = _storage.GetAccounts();
            return Mappers.Mapper.MapAccounts(accounts);
        }

        /// <summary>
        /// Saves the accounts to the storage.
        /// </summary>
        public void Save() => _storage.Save();

        /// <summary>
        /// Reloads the accounts to the storage.
        /// </summary>
        public void Reload() => _storage.LoadAccounts();

        private static void CheckAccount(BankAccount account)
        {
            CheckNotNull(account.Owner, nameof(account.Owner));
            Check(account.Owner.Name, nameof(account.Owner.Name));
            Check(account.Owner.Surname, nameof(account.Owner.Surname));
            CheckNotNull(account.AccountType, nameof(account.AccountType));
            Check(account.AccountType.TypeName, nameof(account.AccountType.TypeName));
        }

        private static void CheckNotNull(object value, string property)
        {
            if (ReferenceEquals(value, null))
            {
                throw new Exception($"The property {property} cannot be null.");
            }
        }

        private static void Check(string value, string property)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception($"The property {property} cannot be null or empty.");
            }
        }
    }
}
