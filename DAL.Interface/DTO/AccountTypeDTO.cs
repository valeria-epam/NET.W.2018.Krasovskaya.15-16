namespace DAL.Interface.DTO
{
    /// <summary>
    /// Represents account type.
    /// </summary>
    public class AccountTypeDTO
    {
        public string TypeName { get; set; }

        public int BalanceCost { get; set; }

        public int RefillCost { get; set; }
    }
}
