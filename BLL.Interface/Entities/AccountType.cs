using System.ComponentModel.DataAnnotations;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// Represents account type.
    /// </summary>
    public class AccountType
    {
        /// <summary>
        /// Gets base account.
        /// </summary>
        public static AccountType BaseAccount { get; } = new AccountType() { TypeName = "Base", BalanceCost = 5, RefillCost = 3 };

        /// <summary>
        /// Gets gold account.
        /// </summary>
        public static AccountType GoldAccount { get; } = new AccountType() { TypeName = "Gold", BalanceCost = 10, RefillCost = 5 };

        /// <summary>
        /// Gets platinum account.
        /// </summary>
        public static AccountType PlatinumAccount { get; } = new AccountType() { TypeName = "Platinum", BalanceCost = 15, RefillCost = 10 };

        [Display(Name = "Account type")]
        public string TypeName { get; set; }

        public int BalanceCost { get; set; }

        public int RefillCost { get; set; }
    }
}
