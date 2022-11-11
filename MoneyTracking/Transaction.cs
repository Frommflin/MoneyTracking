

namespace MoneyTracking
{
    internal class Transaction
    {
        public string TransactionId { get; }
        public string TransactionType { get; set; }
        public string TransactionTitle { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime Date { get; set; }

        private static int TransactionNumberSeed = 234987;

        public Transaction(string transactionId, string transactionType, string transactionTitle, int transactionAmount, DateTime date)
        {
            TransactionId = transactionId;
            TransactionType = transactionType;
            TransactionTitle = transactionTitle;
            TransactionAmount = transactionAmount;
            Date = date;

            TransactionNumberSeed++; //next transaction gets a different id
        }
    }
}
