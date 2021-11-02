using System;
using System.Collections.Generic;
using System.Text;
using static Technovert.Banking.Enums.CLIEnum;

namespace Technovert.Banking.CLI
{
    class CustomerUI
    {
        public static void Customer()
        {
            Console.WriteLine("\t\t ****** Welcome To Bank ******\n\n");

            char flag = 'y';

            do
            {
                Console.WriteLine("\t\t 1. New Customer");
                Console.WriteLine("\t\t 2. Existing Customer");
                Console.WriteLine("\t\t 3. Exit");

                Console.Write("\t\t Enter your choice : ");

                CustomerType userChoice = (CustomerType)Enum.Parse(typeof(CustomerType), Console.ReadLine());

                //MAIN MENU SWITCH
                switch (userChoice)
                {
                    case CustomerType.NewCustomer:
                        NewCustomerUI obj1 = new NewCustomerUI();
                        obj1.NewCustomer();
                        break;
                    case CustomerType.ExistingCustomer:
                        CurrentCustomerUI obj2 = new CurrentCustomerUI();
                        obj2.CurrentCustomer();
                        break;
                    case CustomerType.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\t\t Invalid Choice!!!");
                        break;
                }

                Console.Write("\n\t\t Do you want to perform any more operation?(Y/N) ");
                flag = Console.ReadLine()[0];
            } while (flag == 'y' || flag == 'Y');

            
        }
    }
}
