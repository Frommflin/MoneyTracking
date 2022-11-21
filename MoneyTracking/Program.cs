
using MoneyTracking;

Console.WriteLine("Welcome to your MoneyTracker!");

BankAccount account = new BankAccount(); // account will be used for all transactions
List<Transaction> transactions = new List<Transaction>();
string input;

Methods.CheckForExistingFile();
Methods.LoadTransactions(transactions, account);

do
{
    Console.WriteLine($"Your current balance is {account.Balance}");
    Methods.ShowMenu();

    Console.Write("Enter option: ");
    input = Console.ReadLine();
    input.Trim();
    if (input == "1")
    {
        string listFilter = Methods.FilterList();
        string[] sorting = Methods.SortList();
        Methods.ShowTransactions(transactions, listFilter, sorting);
    }
    else if (input == "2")
    {
        transactions.Add(Methods.AddNewTransaction(account));
    }
    else if (input == "3")
    {
        Methods.ShowTransactionsId(transactions);
        Methods.EditTransaction(transactions);
    }
    else if (input == "4")
    {
        Methods.SaveTransactions(transactions);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid option");
        Console.ResetColor();
    }
    Console.WriteLine();
    Console.WriteLine();
} while (input != "4");
