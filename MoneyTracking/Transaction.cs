

namespace MoneyTracking
{
    internal class Transaction
    {
        public int TransactionId { get; }
        public string TransactionType { get; set; }
        public string TransactionTitle { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime Date { get; set; }
        public BankAccount BankAccount { get; set; }

        private static int transactionNumberSeed = 234987;

        public Transaction(string transactionType, string transactionTitle, int transactionAmount, DateTime date, BankAccount bankAccount)
        {
            TransactionId = transactionNumberSeed;
            TransactionType = transactionType;
            TransactionTitle = transactionTitle;
            TransactionAmount = transactionAmount;
            Date = date;
            BankAccount = bankAccount;

            transactionNumberSeed++; //next transaction gets a different id
            BankAccount.UpdateBalance(transactionAmount);
        }
    }
}
