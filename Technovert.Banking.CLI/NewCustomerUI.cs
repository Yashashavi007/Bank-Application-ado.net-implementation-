using System;
using System.Collections.Generic;
using System.Text;
using static Technovert.Banking.Enums.CustomerEnum;
using Technovert.Banking.Model;
using Technovert.Banking.Service;

namespace Technovert.Banking.CLI
{
    class NewCustomerUI
    {
        public void NewCustomer()
        {
            Console.WriteLine("\n\t\t Fill the form to create a new account.");

            Console.Write("\t\t Enter Bank Name : ");
            string bankName = Console.ReadLine();

            Console.Write("\t\t Enter Bank IFSC code : ");
            string IFSCcode = Console.ReadLine();

            Console.Write("\t\t Enter your name : ");
            string customerName = Console.ReadLine();

            Console.WriteLine("\n\t\t 1. Male ");
            Console.WriteLine("\t\t 2. Female ");
            Console.WriteLine("\t\t 3. Unknown ");
            Console.Write("\n\t\t Enter your Gender : ");
            CustomerGender customerGender = (CustomerGender)Enum.Parse(typeof(CustomerGender), Console.ReadLine());

            Console.Write("\n\t\t Enter initial amount(in Rupees) : ");
            int amount = Convert.ToInt32(Console.ReadLine());



            try
            {
                Customer newCustomer;
                CustomerService obj = new CustomerService();
                //Console.WriteLine("Hello World!!");
                newCustomer = obj.CreateAccount(bankName, IFSCcode, customerName, customerGender, amount);
                Console.WriteLine("\t\t Account created successfully!!");
                Console.WriteLine("\t\t Keep your account details for future reference!");

                Console.WriteLine($"\t\t Account Holder's name : {newCustomer.Name}");
                Console.WriteLine($"\t\t Account Holder's gender : {newCustomer.Gender}");
                Console.WriteLine($"\t\t Account number : {newCustomer.AccountNumber}");
                Console.WriteLine($"\t\t Account pin : {newCustomer.Pin}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



        }
    }
}
