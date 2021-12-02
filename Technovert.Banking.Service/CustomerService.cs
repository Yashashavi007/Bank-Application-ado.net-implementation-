using System;
using System.Collections.Generic;
using System.Text;
using Technovert.Banking.Model;
using static Technovert.Banking.Enums.CustomerEnum;
using static Technovert.Banking.Enums.EmployeeEnum;
using static Technovert.Banking.Enums.TransactionEnum;

namespace Technovert.Banking.Service
{
    public class CustomerService
    {


        //CUSTOMER SERVICE

        Exceptions error = new Exceptions();

        

        public Customer CreateAccount(string bankName, string IFSCCode, string name, CustomerGender gender, int balance)
        {
            return EmployeeService.CreateCustomerAccount(bankName, IFSCCode, name, gender, balance);
        }
        public void Deposit(Bank bank, Customer accNumber, string currency, int amount)
        {
            if (amount > 0)
            {
                float currencyRate = 1;
                if(currency!="INR" || currency!="inr")
                {
                    currencyRate = EmployeeService.GetExchangeRate(currency);
                }
                accNumber.Balance += amount*currencyRate;
                UpdatePassbook(bank, accNumber, accNumber.AccountNumber, TransactionType.Deposit, amount);
            }
            else
            {
                error.DepositError();
            }
        }
        public void Withdraw(Bank bank, Customer accNumber, int amount)
        {
            if (amount <= GetBalance(accNumber))
            {
                accNumber.Balance -= amount;
                UpdatePassbook(bank, accNumber, accNumber.AccountNumber, TransactionType.Withdraw, amount);
            }
            else
            {
                error.WithdrawError();
            }
        }

        public void Transfer(Bank senderbank, Customer fromAccNumber, Bank receiverBank, Customer toAccNumber, float amount)
        {
            EmployeeService.TransferAmount(senderbank, fromAccNumber, receiverBank, toAccNumber, amount);
        }


        public float GetBalance(Customer accNumber)
        {
            return accNumber.Balance;
        }

        public List<Transaction> PassbookPrint(Customer accNumber)
        {
            return accNumber.Passbook;
        }

        public void UpdatePassbook(Bank bank, Customer toAccount, string fromAccount, TransactionType operation, int amount)
        {
            EmployeeService.UpdateTransactionHistory(bank, toAccount, fromAccount, operation, amount);
        }

        public void CheckTableContent()
        {
            EmployeeService.PrintTableContent();
        }
    }
}
