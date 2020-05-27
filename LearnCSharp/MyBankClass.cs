using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCSharp
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance {
            get
            {
                decimal balance = 0;
                foreach (var item in AllTransaction)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }

        private static int AccNumberSeed = 1234567890;
        public List<Transaction> AllTransaction = new List<Transaction>();

        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
            this.Number = AccNumberSeed.ToString();
            AccNumberSeed++;
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <=0 )
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");

            }
            var deposit = new Transaction(amount, date, note);
            AllTransaction.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");


            }
            if (Balance- amount< 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");

            }
            var withdrawal = new Transaction(-amount, date, note);
            AllTransaction.Add(withdrawal);
        }

        public string GetHistory()
        {
            var report = new StringBuilder();
            report.AppendLine($"Date\t\tAmount\tMemo");
            foreach (var item in AllTransaction)
            {
                report.AppendLine ($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Notes}");

            }
            return report.ToString();

        }
    }
}
