using System;
using static Technovert.Banking.Enums.CLIEnum;

namespace Technovert.Banking.CLI
{
    class Welcome
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t Welcome to Banking Application!!");
            char ch = 'y';

            do
            {
                Console.WriteLine("\n\t\t 1. Customer");
                Console.WriteLine("\t\t 2. Employee");
                Console.WriteLine("\t\t 3. Exit");
                Console.Write("\n\t\t Select to continue : ");
                UserType choice = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine());

                switch (choice)
                {
                    case UserType.Customer:
                        CustomerUI.Customer();
                        break;
                    case UserType.Employee:
                        EmployeeUI.Employee();
                        break;
                    case UserType.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\t\t Invalid Choice!!");
                        break;
                }

                Console.Write("\n\t\t Do you want to perform anything else ?.. ");
                ch = Console.ReadLine()[0];
            } while (ch == 'y' || ch == 'Y');
            

            Console.WriteLine("\n\t\t THANK YOU! VISIT AGAIN!!!");
        }
    }
}
