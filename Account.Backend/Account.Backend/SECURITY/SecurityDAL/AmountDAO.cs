using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;


namespace Account.Backend
{
    public class AmountDAO : IDisposable
    {
        private static volatile AmountDAO instance;
        private static readonly object lockObj = new object();
        public static AmountDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new AmountDAO();
            }
            return instance;
        }
        public static AmountDAO GetInstanceThreadSafe
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new AmountDAO();
                        }
                    }
                }
                return instance;
            }
        }

        public void Dispose()
        {
            ((IDisposable)GetInstanceThreadSafe).Dispose();
        }

        DBExecutor dbExecutor;

        public AmountDAO()
        {
            dbExecutor = new DBExecutor();
        }


        public int DepositAmountFromUser(Amount _Amount)
        {
            int ret = 0;
            try
            {
                Parameters[] colparameters = new Parameters[2]{
                new Parameters("@DepositAmount", _Amount.DepositAmount, DbType.Int32, ParameterDirection.Input),
                new Parameters("@Date", DateTime.Now, DbType.Date, ParameterDirection.Input)
                };
                dbExecutor.ManageTransaction(TransactionType.Open);
                ret = dbExecutor.ExecuteScalar32(true, CommandType.StoredProcedure, "DepositedAmountFromUser", colparameters, true);
                dbExecutor.ManageTransaction(TransactionType.Commit);
            }
            catch (DBConcurrencyException except)
            {
                dbExecutor.ManageTransaction(TransactionType.Rollback);
                throw except;
            }
            catch (Exception ex)
            {
                dbExecutor.ManageTransaction(TransactionType.Rollback);
                throw ex;
            }
            return ret;
        }
        public int WithdrawAmountFromUser(Amount _Amount)
        {
            int ret = 0;
            try
            {
                Parameters[] colparameters = new Parameters[2]{
                 new Parameters("@WithdrawalAmount", _Amount.WithdrawalAmount, DbType.Int32, ParameterDirection.Input),
                new Parameters("@Date", DateTime.Now, DbType.Date, ParameterDirection.Input)
                };
                dbExecutor.ManageTransaction(TransactionType.Open);
                ret = dbExecutor.ExecuteScalar32(true, CommandType.StoredProcedure, "WithdrawAmountFromUser", colparameters, true);
                dbExecutor.ManageTransaction(TransactionType.Commit);
            }
            catch (DBConcurrencyException except)
            {
                dbExecutor.ManageTransaction(TransactionType.Rollback);
                throw except;
            }
            catch (Exception ex)
            {
                dbExecutor.ManageTransaction(TransactionType.Rollback);
                throw ex;
            }
            return ret;
        }

        public List<Amount> PrintStatementForUser()
        {
            try
            {
                var ad_DepertmentLst = new List<Amount>();
                ad_DepertmentLst =
                    dbExecutor.FetchData<Amount>(CommandType.StoredProcedure, "Amount_GetAll");
                return ad_DepertmentLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
