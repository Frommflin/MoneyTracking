

namespace MoneyTracking
{
    internal class Transaction
    {
        public int Id { get; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public BankAccount BankAccount { get; set; }

        private static int transactionNumberSeed = 234987;

        public Transaction(string transactionType, string transactionTitle, int transactionAmount, DateTime date, BankAccount bankAccount)
        {
            Id = transactionNumberSeed;
            Type = transactionType;
            Title = transactionTitle;
            Amount = transactionAmount;
            Date = date;
            BankAccount = bankAccount;

            transactionNumberSeed++; //next transaction gets a different id
            BankAccount.UpdateBalance(transactionAmount);
        }

        public Transaction(int id, string type, string title, int amount, DateTime date, BankAccount bankAccount)
        {
            Id = id;
            Type = type;
            Title = title;
            Amount = amount;
            Date = date;
            BankAccount = bankAccount;

            transactionNumberSeed = id + 1;

            BankAccount.UpdateBalance(amount);
        }
    }
}
