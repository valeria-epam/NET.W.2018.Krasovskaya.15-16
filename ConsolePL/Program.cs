using System;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    public class Program
    {
        private static readonly IKernel Resolver;

        static Program()
        {
            Resolver = new StandardKernel();
            Resolver.ConfigurateResolver();
        }

        public static void Main(string[] args)
        {
            var service = Resolver.Get<IAccountManager>();

            var owner = new AccountOwner()
            {
                Name = "Ivan",
                Surname = "Ivanov"
            };

            var type1 = AccountType.BaseAccount;
            var type2 = AccountType.PlatinumAccount;

            var account1 = new BankAccount()
            {
                AccountType = type1,
                Owner = owner,
                Sum = 10m,
                Bonus = 0m,
                State = AccountState.Active
            };

            var account2 = new BankAccount()
            {
                AccountType = type2,
                Owner = owner,
                Sum = 0m,
                Bonus = 0m,
                State = AccountState.Active
            };

            service.AddBankAccount(account1);
            service.AddBankAccount(account2);
            var accounts1 = service.GetAccounts();
            foreach (var bankAccount in accounts1)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            service.RefillAccount(account1, 500m);
            service.RefillAccount(account2, 1000m);

            var accounts2 = service.GetAccounts();

            foreach (var bankAccount in accounts2)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            service.WithdrawalAccount(account2, 1000m);

            var accounts3 = service.GetAccounts();

            foreach (var bankAccount in accounts3)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            service.CloseBankAccount(account2);

            var accounts4 = service.GetAccounts();

            foreach (var bankAccount in accounts4)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            service.Save();
            service.Reload();

            var accounts5 = service.GetAccounts();

            foreach (var bankAccount in accounts5)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
        }
    }
}
