using System;
using System.Collections.Generic;
using System.Text;
using Technovert.Banking.Model;
using static Technovert.Banking.Enums.CustomerEnum;
using static Technovert.Banking.Enums.EmployeeEnum;
using static Technovert.Banking.Enums.TransactionEnum;



namespace Technovert.Banking.Service
{
    public class EmployeeService
    {
        //EMPLOYEE SERVICE
        static List<Bank> BankList = new List<Bank>();
        static Dictionary<string,float> CurrencyValue = new Dictionary<string, float>();
        Exceptions error = new Exceptions();

        //GENERATION
        private static string GenerateBankID(string bankName)
        {
            return (bankName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private static string GenerateAccountID(string accountHolderName)
        {
            return (accountHolderName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy"));
        }

        public static string GenerateTransactionID(string bankId, string accountId)
        {
            return ("TXN" + bankId + accountId + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private static string GenerateAccountNumber()
        {
            var random = new Random();
            string accNo = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                accNo = String.Concat(accNo, random.Next(10).ToString());
            }

            return accNo;
        }

        private static int GeneratePin()
        {
            Random random = new Random();
            return random.Next(1000, 9999);

        }

        //VALIDATION

        public Bank ValidateBank(string bankName)
        {
            foreach (Bank bank in BankList)
            {
                if (bank.Name == bankName)
                {
                    return bank;
                }
            }

            error.BanknotExist();
            return null;
        }

        public Employee ValidateEmployeeAccount(Bank bank, string empID)
        {
            foreach (Employee emp in bank.EmployeeDetails)
            {
                if (emp.ID == empID)
                {
                    return emp;
                }
            }
            error.AccountnotExist();
            return null;
        }
        public void ValidateEmployeePin(Employee emp, int pin)
        {
            if(emp.Pin == pin)
            {
                return;
            }
            else
            {
                error.WrongPin();
            }
        }
        public Customer ValidateAccount(Bank bank, string accNumber)
        {
            foreach (Customer user in bank.CustomerDetails)
            {
                if (user.AccountNumber == accNumber && user.Status == AccountStatus.Active)
                {
                    return user;
                }
            }
            error.AccountnotExist();
            return null;
        }
        public bool ValidateAccountPin(Bank bank, string accNumber, int accPin)
        {
            foreach (Customer user in bank.CustomerDetails)
            {
                if (user.Pin == accPin)
                {
                    return true;
                }
            }
            error.WrongPin();
            return false;
        }

        public Transaction GetTransaction(Customer account, string transactionID)
        {
            foreach(Transaction transaction in account.Passbook)
            {
                if(transaction.Id == transactionID)
                {
                    return transaction;
                }
            }
            error.TransactionNotExist();
            return null;
        }

        //CREATION

        private static Bank CreateBank(string bankName, string IFSCCode)
        {
            Bank bank = new Bank(bankName, IFSCCode);
            bank.BankId = GenerateBankID(bankName);
            BankList.Add(bank);
            return bank;
        }

        public static Customer CreateCustomerAccount(string bankName, string IFSCCode, string name, CustomerGender gender, int balance)
        {
            if (balance < 1000)
            {
                throw new Exception("Account cannot be created!!");
            }
            else
            {
                Bank bank = null;
                foreach (Bank b in BankList)
                {
                    if (b.Name == bankName)
                    {
                        bank = b;
                        break;
                    }
                }
             
                if (bank == null)
                {
                    bank = EmployeeService.CreateBank(bankName, IFSCCode);
                }
                ///Console.WriteLine(bank.Name);
                string accountNumber = GenerateAccountNumber();
                int accountPin = GeneratePin();
                string accountID = GenerateAccountID(name);
                //List<Transaction> passbook = new List<Transaction>();
                
                if(DBService.CheckTable()==false)
                {
                    DBService.CreateCustomerTable();
                }
                
                DBService.InsertIntoTable(accountID, name, Convert.ToString(gender), accountNumber, balance, "open");
                Customer account = new Customer(accountID ,name, gender, accountNumber, accountPin, balance);
                bank.CustomerDetails.Add(account);

                return account;
            }

        }

        public Employee CreateEmployeeAccount(string bankName, string IFSCCode, string name, EmployeeGender gender, Role role)
        {
            Bank bank = null;
            foreach (Bank b in BankList)
            {
                if (b.Name == bankName)
                {
                    bank = b;
                    break;
                }
            }
            
            if (bank == null)
            {
                bank = CreateBank(bankName, IFSCCode);
            }
            ///Console.WriteLine(bank.Name);
            string accountId = GenerateAccountID(name);
            int accountPin = GeneratePin();
            //List<Transaction> passbook = new List<Transaction>();

            Employee account = new Employee(accountId, name, gender, role, accountPin);


            bank.EmployeeDetails.Add(account);

            return account;


        }

        //OPERATIONS

        public void UpdateName(Customer account, string name)
        {
            account.Name = name;
        }

        public int UpdatePin(Customer account)
        {
            account.Pin = GeneratePin();
            return account.Pin;
        }
        public void DeleteAccount(Customer account)
        {
            account.Status = AccountStatus.Closed;
        }

        public void RevertTransaction(Customer account, Transaction transaction)
        {         
            if(transaction.type == TransactionType.Deposit)
            {
                account.Balance -= transaction.Amount;
            }
            else if(transaction.type == TransactionType.Withdraw)
            {
                account.Balance += transaction.Amount;
            }
        }

        public void RevertTransaction(Customer sender, Customer receiver, Transaction transaction)
        {
            sender.Balance += transaction.Amount;
            receiver.Balance -= transaction.Amount;
        }

        public static float GetExchangeRate(string currency)
        {
            if(CurrencyValue.ContainsKey(currency) == false)
            {
                Console.Write($"\n\t\t Enter {currency} exchange rate : ");
                float rate = Single.Parse(Console.ReadLine());
                CurrencyValue.Add(currency, rate);
            }
            return CurrencyValue[currency];
        }

        public static void TransferAmount(Bank senderbank, Customer fromAccNumber, Bank receiverBank, Customer toAccNumber, float amount)
        {
            if (amount <= fromAccNumber.Balance)
            {
                fromAccNumber.Balance -= amount;
                toAccNumber.Balance += amount;
                if (amount >= 100000)
                {
                    if(senderbank != receiverBank)
                    {
                        fromAccNumber.Balance -= amount * senderbank.DiffRTGS;
                    }
                    
                }
                else
                {
                    if(senderbank == receiverBank)
                    {
                        fromAccNumber.Balance -= amount * senderbank.SameIMPS;
                    }
                    else
                    {
                        fromAccNumber.Balance -= amount * senderbank.DiffIMPS;
                    }
                }
                

                //BankService Manager = new BankService();
                UpdateTransactionHistory(senderbank, fromAccNumber, toAccNumber.AccountNumber, TransactionType.Transfer, amount);
                UpdateTransactionHistory(receiverBank, toAccNumber, fromAccNumber.AccountNumber, TransactionType.Transfer, amount);
            }
            else
            {
                Exceptions.TransferError();
            }
        }

        public void PrintTransactionHistory(Customer accNumber)
        {
            foreach(Transaction transaction in accNumber.Passbook)
            {
                Console.WriteLine($"{transaction.Id} | {transaction.SenderAccountNumber} | {transaction.timeStamp} | {transaction.type} | {transaction.Amount}");
            }
        }
        public static void UpdateTransactionHistory(Bank bank, Customer toAccount, string fromAccount, TransactionType operation, float amount)
        {
            string transactionId = GenerateTransactionID(bank.Name, toAccount.Name);
            Transaction obj = new Transaction(operation, fromAccount, amount, DateTime.Now);
            obj.Id = transactionId;
            toAccount.Passbook.Add(obj);
        }

        public static void PrintTableContent()
        {
            DBService.RetrieveData();
        }
    }
}
