using System;
using System.Collections.Generic;
using System.Text;
using static Technovert.Banking.Enums.EmployeeEnum;

namespace Technovert.Banking.Model
{
    public class Employee : User
    {
        public EmployeeGender Gender { get; set; }
        public Role ERole { get; set; }
        public int Pin { get; set; }

        public Employee(string eID, string Name, EmployeeGender gender, Role role, int pin)
        {
            this.ID = eID;
            this.Name = Name;
            this.Gender = gender;
            this.ERole = role;
            this.Pin = pin;
        }
    }
}
