using System;
using System.Collections.Generic;
using System.Text;
using static Technovert.Banking.Enums.TransactionEnum;

namespace Technovert.Banking.Model
{
    public class Transaction
    {
        public string Id { get; set; }

        public TransactionType type { get; set; }

        public DateTime timeStamp { get; set; }

        public string SenderAccountNumber { get; set; }

        public float Amount { get; set; }

        public Transaction(TransactionType type, string accountNumber, float amount, DateTime time)
        {
            this.type = type;
            this.SenderAccountNumber = accountNumber;
            this.Amount = amount;
            this.timeStamp = time;
        }
    }
}
