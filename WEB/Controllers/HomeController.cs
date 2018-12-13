using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using WEB.Models;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IAccountTypeManager _typeManager;

        public HomeController(IAccountManager accountManager, IAccountTypeManager typeManager)
        {
            this._accountManager = accountManager;
            this._typeManager = typeManager;
        }

        public ActionResult Index()
        {
            var accounts = this._accountManager.GetAccounts();
            return this.View(accounts);
        }

        public ActionResult CreateAccount()
        {
            SelectList states = new SelectList(new[] { AccountState.Active, AccountState.Closed });
            ViewBag.States = states;
            SelectList types = new SelectList(this._typeManager.GetAccountTypes(), "TypeName", "TypeName");
            ViewBag.Types = types;

            return this.View();
        }

        [HttpPost]
        public ActionResult CreateAccount(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = new AccountOwner() { Name = model.Name, Surname = model.Surname };
                var accountType = this._typeManager.GetAccountTypes().First(type => type.TypeName == model.AccountType);
                var account = new BankAccount()
                {
                    Owner = owner,
                    Email = model.Email,
                    Sum = model.Sum,
                    State = model.State,
                    Bonus = model.Bonus,
                    AccountType = accountType
                };

                try
                {
                    this._accountManager.AddBankAccount(account);
                    return this.RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return this.View(model);
        }

        public ActionResult Refill()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Refill(RefillAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = this._accountManager.GetAccount(model.Number);
                if (account != null)
                {
                    try
                    {
                        this._accountManager.RefillAccount(account, model.AmountOfMoney);
                        return this.RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Number), "Your account doesn't exist");
                }
            }

            return this.View(model);
        }

        public ActionResult Withdrawal()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Withdrawal(RefillAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = this._accountManager.GetAccount(model.Number);
                if (account != null)
                {
                    try
                    {
                        this._accountManager.WithdrawalAccount(account, model.AmountOfMoney);
                        return this.RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Number), "Your account doesn't exist");
                }
            }

            return this.View(model);
        }

        public ActionResult Close(string id)
        {
            var account = this._accountManager.GetAccount(id);
            if (account != null)
            {
                this._accountManager.CloseBankAccount(account);
            }

            return this.RedirectToAction("Index");
        }

        public ActionResult Transfer()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Transfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountFrom = this._accountManager.GetAccount(model.NumberFrom);
                var accountTo = this._accountManager.GetAccount(model.NumberTo);
                if (accountFrom != null && accountTo != null)
                {
                    try
                    {
                        this._accountManager.Transfer(accountFrom, accountTo, model.AmountOfMoney);
                        return this.RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                else
                {
                    if (accountFrom == null)
                    {
                        ModelState.AddModelError(nameof(model.NumberFrom), "Your account doesn't exist");
                    }

                    if (accountTo == null)
                    {
                        ModelState.AddModelError(nameof(model.NumberTo), "Your account doesn't exist");
                    }
                }
            }

            return this.View(model);
        }
    }
}