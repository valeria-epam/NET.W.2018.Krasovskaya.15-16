using System;

namespace DAL.Interface.DTO
{
    /// <summary>
    /// Represents a bank account.
    /// </summary>
    public class BankAccountDTO : IEquatable<BankAccountDTO>
    {
        public string Number { get; set; }

        public AccountOwnerDTO Owner { get; set; }

        public decimal Sum { get; set; }

        public decimal Bonus { get; set; }

        public AccountTypeDTO AccountType { get; set; }

        public AccountStateDTO State { get; set; }

        public static bool operator ==(BankAccountDTO left, BankAccountDTO right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(BankAccountDTO left, BankAccountDTO right)
        {
            return !object.Equals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(BankAccountDTO other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Number, other.Number);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return this.Equals((BankAccountDTO)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = Number != null ? Number.GetHashCode() : 0;
            return hashCode;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Number)}: {Number}, {nameof(Owner)}: {Owner.Name} {Owner.Surname}, {nameof(Sum)}: {Sum}," +
                   $" {nameof(Bonus)}: {Bonus}, {nameof(AccountType)}: {AccountType.TypeName}, {nameof(State)}: {State}";
        }
    }
}
