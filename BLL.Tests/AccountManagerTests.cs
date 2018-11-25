using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.ServiceImplementation;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class AccountManagerTests
    {
        [Test]
        public void AddBankAccount()
        {
            var generator = new Mock<IGenerateNumber>();
            var calculate = new Mock<ICalculateBonus>();
            var storage = new Mock<IAccountStorage>();

            var service = new AccountManager(calculate.Object, storage.Object, generator.Object);

            var owner = new AccountOwner()
            {
                Name = "Ivan",
                Surname = "Ivanov"
            };

            var type1 = AccountType.BaseAccount;

            var account1 = new BankAccount()
            {
                AccountType = type1,
                Owner = owner,
                Sum = 10m,
                Bonus = 0m,
                State = AccountState.Active
            };

            service.AddBankAccount(account1);

            generator.Verify(number => number.Generate(), Times.Exactly(1));
            storage.Verify(accountStorage => accountStorage.AddAccount(It.IsAny<BankAccountDTO>()), Times.Exactly(1));
            storage.Verify(accountStorage => accountStorage.AccountExists(It.IsAny<BankAccountDTO>()), Times.Exactly(1));
        }

        [Test]
        public void GetAccounts()
        {
            var generator = new Mock<IGenerateNumber>();
            var calculate = new Mock<ICalculateBonus>();
            var storage = new Mock<IAccountStorage>();

            var service = new AccountManager(calculate.Object, storage.Object, generator.Object);
            storage.Setup(accountStorage => accountStorage.GetAccounts()).Returns(new List<BankAccountDTO>() { new BankAccountDTO() });

            Assert.IsNotEmpty(service.GetAccounts());
        }

        [Test]
        public void Save()
        {
            var generator = new Mock<IGenerateNumber>();
            var calculate = new Mock<ICalculateBonus>();
            var storage = new Mock<IAccountStorage>();

            var service = new AccountManager(calculate.Object, storage.Object, generator.Object);
            service.Save();

            storage.Verify(accountStorage => accountStorage.Save(), Times.Exactly(1));
        }

        [Test]
        public void GetAccount()
        {
            var generator = new Mock<IGenerateNumber>();
            var calculate = new Mock<ICalculateBonus>();
            var storage = new Mock<IAccountStorage>();

            var service = new AccountManager(calculate.Object, storage.Object, generator.Object);
            storage.Setup(accountStorage => accountStorage.GetAccount(It.IsAny<string>())).Returns(new BankAccountDTO());

            Assert.IsNotNull(service.GetAccount("ff"));
        }

        [Test]
        public void RefillAccount()
        {
            var generator = new Mock<IGenerateNumber>();
            var calculate = new Mock<ICalculateBonus>();
            var storage = new Mock<IAccountStorage>();

            var service = new AccountManager(calculate.Object, storage.Object, generator.Object);

            var owner = new AccountOwner()
            {
                Name = "Ivan",
                Surname = "Ivanov"
            };

            var type1 = AccountType.BaseAccount;

            var account1 = new BankAccount()
            {
                AccountType = type1,
                Owner = owner,
                Sum = 10m,
                Bonus = 0m,
                State = AccountState.Active
            };

            storage.Setup(accountStorage => accountStorage.GetAccount(It.IsAny<string>()))
                .Returns(Mappers.Mapper.MapAccountToDTO(account1));

            service.RefillAccount(account1, 4);

            Assert.AreEqual(account1.Sum, 14m);
            calculate.Verify(bonus => bonus.RefillBonus(account1, 4m), Times.Exactly(1));
        }

        [Test]
        public void WithdrawalAccount()
        {
            var generator = new Mock<IGenerateNumber>();
            var calculate = new Mock<ICalculateBonus>();
            var storage = new Mock<IAccountStorage>();

            var service = new AccountManager(calculate.Object, storage.Object, generator.Object);

            var owner = new AccountOwner()
            {
                Name = "Ivan",
                Surname = "Ivanov"
            };

            var type1 = AccountType.BaseAccount;

            var account1 = new BankAccount()
            {
                AccountType = type1,
                Owner = owner,
                Sum = 10m,
                Bonus = 0m,
                State = AccountState.Active
            };

            storage.Setup(accountStorage => accountStorage.GetAccount(It.IsAny<string>()))
                .Returns(Mappers.Mapper.MapAccountToDTO(account1));

            service.WithdrawalAccount(account1, 4);

            Assert.AreEqual(account1.Sum, 6m);
            calculate.Verify(bonus => bonus.WithdrawalBonus(account1, 4m), Times.Exactly(1));
        }
    }
}
