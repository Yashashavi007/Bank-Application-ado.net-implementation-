using System;
using System.Collections.Generic;
using System.Text;

namespace Technovert.Banking.Model
{
    public class Bank
    {
        public string BankId { get; set; }
        public string IFSCCode { get; set; }
        public string Name { get; set; }
        public List<Customer> CustomerDetails { get; set; }
        public List<Employee> EmployeeDetails { get; set; }

        private float sameRTGS = 0;
        private float diffRTGS = 2/100;
        private float sameIMPS = 5/100;
        private float diffIMPS = 6/100;

        public float SameRTGS
        {
            get
            {
                return sameRTGS;
            }
            set
            {
                sameRTGS = value;
            }
        }
        public float DiffRTGS
        {
            get
            {
                return diffRTGS;
            }
            set
            {
                diffRTGS = value;
            }
        }
        public float SameIMPS
        {
            get
            {
                return sameIMPS;
            }
            set
            {
                sameIMPS = value;
            }
        }
        public float DiffIMPS
        {
            get
            {
                return diffIMPS;
            }
            set
            {
                diffIMPS = value;
            }
        }


        private string currency = "INR";
        public string Currency
        {
            get { return currency; }
            set
            {
                currency = value;
            }
        }




        public Bank(string name, string IFSCCode)
        {
            this.Name = name;
            this.IFSCCode = IFSCCode;
            CustomerDetails = new List<Customer>();
            EmployeeDetails = new List<Employee>();
        }
    }
}
