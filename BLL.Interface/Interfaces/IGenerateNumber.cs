namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface logic for generate bank account number.
    /// </summary>
    public interface IGenerateNumber
    {
        /// <summary>
        /// Generates the bank account number.
        /// </summary>
        string Generate();
    }
}
