using Account.Backend;
using Account.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Account.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Deposit()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Deposit(Amount amount)
        {
            if (amount.DepositAmount <= 0)
                throw new ArgumentOutOfRangeException("Deposite amount can not be Zero(0) or less then Zero(0)");
            _accountService.Deposit(amount);
            return RedirectToAction("PrintStatement");

        }
        [HttpGet]
        public IActionResult Withdraw()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Withdraw(Amount amount)
        {
            var result = _accountService.PrintStatement().OrderByDescending(x => x.Id).Select(x => x.Balance).FirstOrDefault();
            if (amount.WithdrawalAmount <= 0)
                throw new ArgumentOutOfRangeException("Withdrawal amount can not be Zero(0) or less then Zero(0)");
            if (amount.WithdrawalAmount > result)
                throw new ArgumentOutOfRangeException("Withdrawal amount can not greater then balance amount");

            _accountService.Withdraw(amount);
            return RedirectToAction("PrintStatement");

        }
        public IActionResult PrintStatement()
        {
            var result = _accountService.PrintStatement();
            return View(result);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
