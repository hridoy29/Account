using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Account.Backend
{
    public class AmountBLL
    {
        public AmountBLL()
        {
             AmountDAO = new AmountDAO();
        }

        public AmountDAO AmountDAO { get; set; }

        public int DepositAmountFromUser(Amount amount)
        {
            try
            {
                return AmountDAO.DepositAmountFromUser(amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int WithdrawAmountFromUser(Amount amount)
        {
            try
            {
                return AmountDAO.WithdrawAmountFromUser(amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Amount> PrintStatementForUser()
        {
            try
            {
                return AmountDAO.PrintStatementForUser();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
