
using MoneyTracking;

Console.WriteLine("Welcome to your MoneyTracker!");

BankAccount account = new BankAccount(); // account will be used for all transactions
List<Transaction> transactions = new List<Transaction>();
string input;

do
{
    Methods.ShowMenu();

    Console.Write("Enter option: ");
    input = Console.ReadLine();
    input.Trim();
    if (input == "1")
    {
        Methods.ShowTransactions();
    }
    else if (input == "2")
    {
        transactions.Add(Methods.AddNewTransaction(account));
    }
    else if (input == "3")
    {
        Methods.EditTransaction();
    }
    else if (input == "4")
    {
        Methods.SaveTransactions();
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
