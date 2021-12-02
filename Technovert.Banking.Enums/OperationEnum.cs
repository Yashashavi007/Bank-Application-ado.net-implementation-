using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.Banking.Enums
{
    public class OperationEnum
    {
        public enum CustomerOperations
        {
            Deposit = 1,
            Withdraw,
            Transfer,
            CheckBalance,
            PrintPassbook,
            PrintTableContent

        }

        public enum EmployeeOperations
        {
            Update = 1,
            Delete,
            Revert,
            ViewTransactionHistory
        }

        public enum UpdateOperations
        {
            Name = 1,
            Pin
        }
    }
}
