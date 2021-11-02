using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.Banking.Enums
{
    public class CLIEnum
    {
        public enum CustomerType
        {
            NewCustomer = 1,
            ExistingCustomer,
            Exit
        }

        public enum EmployeeType
        {
            NewEmployee = 1,
            ExistingEmployee,
            Exit
        }

        public enum UserType
        {
            Customer = 1,
            Employee,
            Exit
        }
    }
}
