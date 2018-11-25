using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface logic for calculation of bonuses.
    /// </summary>
    public interface ICalculateBonus
    {
        /// <summary>
        /// Calculates bonus for the refill operation.
        /// </summary>
        decimal RefillBonus(BankAccount account, decimal amountOfMoney);

        /// <summary>
        /// Calculates bonus for the withdraw operation.
        /// </summary>
        decimal WithdrawalBonus(BankAccount account, decimal amountOfMoney);
    }
}
