using System;
using System.Collections.Generic;
using System.Text;
using Technovert.Banking.Service;
using Technovert.Banking.Model;
using static Technovert.Banking.Enums.OperationEnum;

namespace Technovert.Banking.CLI
{
    class CurrentCustomerUI
    {
        public void CurrentCustomer()
        {
            EmployeeService Manager = new EmployeeService();
            CustomerService Customer = new CustomerService();
            Customer User = null;
            Bank bank = null;
            string accountNumber, bankName;
            int accountPin;

            do
            {
                Console.Write("\n\t\t Enter Bank Name : ");
                bankName = Console.ReadLine();
                try
                {
                    bank = Manager.ValidateBank(bankName);

                    Console.Write("\n\t\t Enter Account Number : ");
                    accountNumber = Console.ReadLine();

                    try
                    {
                        User = Manager.ValidateAccount(bank, accountNumber);

                        bool pinCheck = false;
                        int attempt = 3;
                        try
                        {

                            do
                            {
                                Console.Write("\n\t\t Enter Pin : ");
                                accountPin = Convert.ToInt32(Console.ReadLine());
                                if (User.Pin == accountPin)
                                {
                                    pinCheck = true;
                                    break;
                                }
                            } while (attempt > 0);

                            if (pinCheck == false)
                            {
                                User = null;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            attempt--;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("\t\tPlease try again!!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (User == null);



            char ch = 'y';
            do
            {
                Console.WriteLine("\n\t\t <-- Operation List -->");
                Console.WriteLine("\t\t 1. Deposit Amount");
                Console.WriteLine("\t\t 2. Withdraw Amount");
                Console.WriteLine("\t\t 3. Transfer Amount");
                Console.WriteLine("\t\t 4. Check Balance");
                Console.WriteLine("\t\t 5. Print e-Passbook");

                Console.Write("\t\t What you would like to do today? ");
                //int choice = Convert.ToInt32(Console.ReadLine());
                CustomerOperations userChoice = (CustomerOperations)Enum.Parse(typeof(CustomerOperations), Console.ReadLine());

                switch (userChoice)
                {
                    case CustomerOperations.Deposit:
                        try
                        {
                            Console.Write("\t\t Enter Currency : ");
                            string currency = Console.ReadLine();

                            Console.Write("\t\t Enter Amount : ");
                            int dAmount = Convert.ToInt32(Console.ReadLine());
                            Customer.Deposit(bank, User, currency, dAmount);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CustomerOperations.Withdraw:
                        try
                        {
                            Console.Write("\t\t Enter Amount : ");
                            int wAmount = Convert.ToInt32(Console.ReadLine());
                            Customer.Withdraw(bank, User, wAmount);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CustomerOperations.Transfer:
                        Console.Write("\n\t\t Enter Receiver's Bank : ");
                        string rBank = Console.ReadLine();
                        try
                        {
                            Bank receiverBank = Manager.ValidateBank(rBank);

                            Console.Write("\n\t\t Enter Receiver's Account number : ");
                            string rAccountNumber = Console.ReadLine();

                            Customer rAccount = Manager.ValidateAccount(receiverBank, rAccountNumber);

                            try
                            {
                                Manager.ValidateAccount(receiverBank, rAccountNumber);
                                Console.Write("\t\t Enter Amount : ");
                                int tAmount = Convert.ToInt32(Console.ReadLine());
                                Customer.Transfer(bank, User, receiverBank, rAccount, tAmount);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case CustomerOperations.CheckBalance:
                        Console.WriteLine($"\t\t Current balance : {Customer.GetBalance(User)}");
                        break;
                    case CustomerOperations.PrintPassbook:
                        Console.Write("\t\t Transaction History : ");
                        foreach (Transaction transaction in Customer.PassbookPrint(User))
                        {
                            Console.WriteLine($"\t\t {transaction.Id} | {transaction.SenderAccountNumber} | {transaction.timeStamp} | {transaction.Amount}");
                        }
                        break;
                    default:
                        Console.WriteLine("\t\t Invalid Choice!!");
                        break;
                }

                Console.Write("\n\t\t Would you like to perform more operations? (Y/N) : ");
                ch = Console.ReadLine()[0];
            } while (ch == 'y' || ch == 'Y');

        }
    }
}
