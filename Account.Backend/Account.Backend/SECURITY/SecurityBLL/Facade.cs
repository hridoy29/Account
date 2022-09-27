using DbExecutor;
 
namespace Account.Backend
{
    public static class Facade
    {

        public static AmountBLL AmountBLL { get { return new AmountBLL(); } }
        public static error_LogBLL ErrorLog { get { return new error_LogBLL(); } }
       
      
       
      
        
    }
}