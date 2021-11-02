using System;
using System.Collections.Generic;
using System.Text;
using Technovert.Banking.Service;
using Technovert.Banking.Model;
using static Technovert.Banking.Enums.OperationEnum;


namespace Technovert.Banking.CLI
{
    class CurrentEmployeeUI
    {
        public void CurrentEmployee()
        {
            try
            {
                EmployeeService Emp = new EmployeeService();
                Console.WriteLine("\t\t ****** Welcome To Bank ******\n\n");

                Console.Write("\n\t\t Enter Bank name : ");
                string bank = Console.ReadLine();

                Bank empBank = Emp.ValidateBank(bank);

                Console.Write("\n\t\t Enter your employee Id : ");
                string empID = Console.ReadLine();

                Employee employee = Emp.ValidateEmployeeAccount(empBank, empID);

                Console.Write("\n\t\t Enter your pin : ");
                int pin = Convert.ToInt32(Console.ReadLine());

                Emp.ValidateEmployeePin(employee, pin);

                char ch1 = 'y';
                do
                {
                    Console.WriteLine("\t\t ######### SERVICES #########");
                    Console.WriteLine("\t\t 1. Update Account Details");
                    Console.WriteLine("\t\t 2. Delete Account");
                    Console.WriteLine("\t\t 3. Revert Transaction");
                    Console.WriteLine("\t\t 4. View Transaction History");

                    Console.WriteLine("\n\t\t What would you like to do ?");
                    Console.Write("\n Enter your choice: ");
                    EmployeeOperations ch = (EmployeeOperations)Enum.Parse(typeof(EmployeeOperations), Console.ReadLine());
                    Console.Write("\n\t\t Enter account number : ");
                    string accountNumber = Console.ReadLine();
                    Customer account = Emp.ValidateAccount(empBank, accountNumber);


                    switch (ch)
                    {
                        case EmployeeOperations.Update:
                            Console.WriteLine("\t\t 1. Name");
                            Console.WriteLine("\t\t 2. Pin");
                            Console.Write("\n\t\t Enter your choice :");
                            UpdateOperations choice = (UpdateOperations)Enum.Parse(typeof(UpdateOperations), Console.ReadLine());

                            if (choice == UpdateOperations.Name)
                            {
                                Console.Write("Enter Name : ");
                                string newName = Console.ReadLine();
                                Emp.UpdateName(account, newName);
                            }
                            else if (choice == UpdateOperations.Pin)
                            {
                                int newPin = Emp.UpdatePin(account);
                                Console.WriteLine($"\t\t New Pin : {newPin}");
                            }
                            break;
                        case EmployeeOperations.Delete:
                            Emp.DeleteAccount(account);
                            break;
                        case EmployeeOperations.Revert:
                            Console.Write("\n\t\t Enter transaction ID : ");
                            string transactionID = Console.ReadLine();
                            Transaction transaction = Emp.GetTransaction(account, transactionID);
                            Emp.RevertTransaction(account, transaction);
                            break;
                        case EmployeeOperations.ViewTransactionHistory:
                            Emp.PrintTransactionHistory(account);
                            break;
                        default:
                            Console.WriteLine("\t\t Invalid Choice!!!");
                            break;
                    }
                } while (ch1 == 'y' || ch1 == 'Y');
                

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
