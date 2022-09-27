using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Account.Backend
{
	public class Amount
	{
        public int Id { get; set; }
         public int Balance { get; set; }
        [Range(1, 10000000, ErrorMessage = "Value for Deposit Amount must be between {1} and {2}.")]
        public int DepositAmount { get; set; }
        [Range(1, 10000000, ErrorMessage = "Value for Withdrawal Amount must be between {1} and {2}.")]
        public int WithdrawalAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
