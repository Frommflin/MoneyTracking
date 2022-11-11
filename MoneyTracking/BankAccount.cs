
namespace MoneyTracking
{
    internal class BankAccount
    {
        public int Balance { get; set; }
        public BankAccount()
        {
            Balance = 0; //Account starts empty
        }

        public void UpdateBalance(int amount)
        {
            Balance += amount;
        }
    }
}
