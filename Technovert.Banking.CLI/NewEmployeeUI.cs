using System;
using System.Collections.Generic;
using System.Text;
using static Technovert.Banking.Enums.EmployeeEnum;
using Technovert.Banking.Service;
using Technovert.Banking.Model;

namespace Technovert.Banking.CLI
{
    class NewEmployeeUI
    {
        public void NewEmployee()
        {
            Console.WriteLine("\n\t\t Fill the form to create a new account.");

            Console.Write("\t\t Enter Bank Name : ");
            string bankName = Console.ReadLine();

            Console.Write("\t\t Enter Bank IFSC code : ");
            string IFSCcode = Console.ReadLine();

            Console.Write("\t\t Enter your name : ");
            string name = Console.ReadLine();

            Console.WriteLine("\n\t\t 1. Male ");
            Console.WriteLine("\t\t 2. Female ");
            Console.WriteLine("\t\t 3. Unknown ");
            Console.Write("\n\t\t Enter your Gender : ");
            EmployeeGender gender = (EmployeeGender)Enum.Parse(typeof(EmployeeGender), Console.ReadLine());

            Console.WriteLine("\n\t\t 1. Manager ");
            Console.WriteLine("\t\t 2. Cashier ");
            Console.WriteLine("\t\t 3. Helper ");
            Console.Write("\t\t Enter your role : ");
            Role employeeRole = (Role)Enum.Parse(typeof(Role), Console.ReadLine());
            





            try
            {
                Employee newEmployee;
                EmployeeService obj = new EmployeeService();
                //Console.WriteLine("Hello World!!");
                newEmployee = obj.CreateEmployeeAccount(bankName, IFSCcode, name, gender, employeeRole);
                Console.WriteLine("\t\t Account created successfully!!");
                Console.WriteLine("\t\t Keep your account details for future reference!");

                Console.WriteLine($"\t\t Employee's name : {newEmployee.Name}");
                Console.WriteLine($"\t\t Employee's gender : {newEmployee.Gender}");
                Console.WriteLine($"\t\t Employee's ID : {newEmployee.ID}");
                Console.WriteLine($"\t\t Employee's PIN : {newEmployee.Pin}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
