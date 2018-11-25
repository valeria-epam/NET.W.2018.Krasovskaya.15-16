using BLL.Interface.Entities;
using BLL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Represents logic for calculation of bonuses.
    /// </summary>
    public class CalculateBonus : ICalculateBonus
    {
        /// <summary>
        /// Calculates bonus for the refill operation.
        /// </summary>
        public decimal RefillBonus(BankAccount account, decimal amountOfMoney)
        {
            return amountOfMoney * account.AccountType.RefillCost / 100;
        }

        /// <summary>
        /// Calculates bonus for the withdraw operation.
        /// </summary>
        public decimal WithdrawalBonus(BankAccount account, decimal amountOfMoney)
        {
            return amountOfMoney * account.AccountType.BalanceCost / 100;
        }
    }
}
