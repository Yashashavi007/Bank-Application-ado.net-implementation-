using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.Banking.Service
{
    class Exceptions
    {
        public Exception WrongPin()
        {
            throw new Exception("\t\tWrong Pin!!");
        }

        public Exception BanknotExist()
        {
            throw new Exception("\t\tBank doesn't Exist!!!");
        }
        public void AccountnotExist()
        {
            throw new Exception("\t\tAccount doesn't Exist!!!");
        }
        //public void AccountnotCreated()
        //{
        //    throw new Exception("Account cannot be created!!");
        //}
        public void TransactionNotExist()
        {
            throw new Exception("\t\t Invalid Transaction ID!!!");
        }
        public void DepositError()
        {
            throw new Exception("\t\tAmount cannot be deposited!");
        }

        public void WithdrawError()
        {
            throw new Exception("\t\tInsufficient Balance!!");
        }
        public static void TransferError()
        {
            throw new Exception("\t\tMoney cannot be transferred!!");
        }
    }
}
