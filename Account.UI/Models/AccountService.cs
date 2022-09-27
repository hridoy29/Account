using Account.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.UI.Models
{
    public class AccountService : IAccountService
    {
        public void Deposit(Amount amount)
        {
             Facade.AmountBLL.DepositAmountFromUser(amount);
        }
        public void Withdraw(Amount amount)
        {
            Facade.AmountBLL.WithdrawAmountFromUser(amount);
        }
        public List<Amount> PrintStatement()
        {
            return Facade.AmountBLL.PrintStatementForUser();
        }

       
    }
}
