using System;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// Represents a bank account.
    /// </summary>
    public class BankAccount : IEquatable<BankAccount>
    {
        public string Number { get; set; }

        public string Email { get; set; }

        public AccountOwner Owner { get; set; }

        public decimal Sum { get; set; }

        public decimal Bonus { get; set; }

        public AccountType AccountType { get; set; }

        public AccountState State { get; set; }

        public static bool operator ==(BankAccount left, BankAccount right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(BankAccount left, BankAccount right)
        {
            return !object.Equals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(BankAccount other)
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

            return this.Equals((BankAccount)obj);
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