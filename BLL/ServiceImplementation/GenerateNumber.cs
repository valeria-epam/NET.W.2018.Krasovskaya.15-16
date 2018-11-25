using System;
using BLL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Represents logic for generate bank account number.
    /// </summary>
    public class GenerateNumber : IGenerateNumber
    {
        /// <summary>
        /// Generates the bank account number.
        /// </summary>
        public string Generate()
        {
            var result = $"{Guid.NewGuid():N}";
            return result;
        }
    }
}
