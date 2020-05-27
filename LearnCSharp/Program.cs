using System;
using System.Collections.Generic;

namespace LearnCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Account1 = new BankAccount("Bunlue", 10000);

            Console.WriteLine($"Account #{Account1.Number} have been created for {Account1.Owner} with balance of {Account1.Balance}");

            Account1.MakeWithdrawal(400, DateTime.Now, "Test 1");
            Console.WriteLine(Account1.Balance);

            Console.WriteLine(Account1.GetHistory());
        }
    }
}
