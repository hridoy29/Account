using Account.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.UI.Models
{
   public interface IAccountService
    {
        void Deposit(Amount amount);
        void Withdraw(Amount amount);
        List<Amount> PrintStatement();
    }
}
