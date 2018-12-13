using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Interface for storing account information.
    /// </summary>
    public interface IAccountStorage
    {
        /// <summary>
        /// Checks if the account exists.
        /// </summary>
        bool AccountExists(BankAccountDTO account);

        /// <summary>
        /// Adds the account to the storage.
        /// </summary>
        void AddAccount(BankAccountDTO account);

        /// <summary>
        /// Delete the account with the specified <paramref name="number"/> from the storage.
        /// </summary>
        void DeleteAccount(string number);

        /// <summary>
        /// Gets the account with the specified <paramref name="number"/>.
        /// </summary>
        BankAccountDTO GetAccount(string number);

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        IEnumerable<BankAccountDTO> GetAccounts();

        /// <summary>
        /// Gets all account types.
        /// </summary>
        IEnumerable<AccountTypeDTO> GetAccountTypes();

        /// <summary>
        /// Saves the accounts to the storage.
        /// </summary>
        void Save();

        /// <summary>
        /// Loads the accounts.
        /// </summary>
        void LoadAccounts();
    }
}
