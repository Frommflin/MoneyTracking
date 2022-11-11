
namespace MoneyTracking
{
    internal class Methods
    {
        public static void ShowMenu()
        {
            string[] menu = { "Show transactions", "Add new transaction", "Edit transaction", "Save and exit" };
            
            Console.WriteLine("Menu: Type the number for the desired option to move on");
            Console.WriteLine("****************");
            
            for (int i = 0; i < menu.Length; i++)
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(i+1);
                Console.ResetColor();
                Console.WriteLine($") {menu[i]}");
            }
        }

        public static void ShowTransactions()
        {
            //TODO
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This option is under construction");
            Console.ResetColor();
        }

        public static Transaction AddNewTransaction(BankAccount account)
        {
            //TODO
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This option is under construction");
            Console.ResetColor();

            string input;
            string type;
            string title;
            int amount;
            DateTime date;

            Console.WriteLine("Follow instructions to add a new transaction");
            Console.Write($"Type of transaction (income/expense): ");
            input = Console.ReadLine();
            input.Trim().ToLower();
            if(input == "income" || input == "expense")
            {
                type = input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid transaction type.");
                Console.ResetColor();
                return null;
            }

            Console.Write($"Title of transaction: ");
            input = Console.ReadLine();
            input.Trim();
            if (!String.IsNullOrEmpty(input))
            {
                title = input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Title cannot be empty.");
                Console.ResetColor();
                return null;
            }

            Console.Write($"Amount: ");
            input = Console.ReadLine();
            input.Trim();
            if (int.TryParse(input, out int value))
            {
                if(value > 0)
                {
                    if(type == "expense")
                    {
                        amount = -value; // stores negative value
                    }
                    else // type == income
                    {
                        amount = value;
                    }
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid amount. Only integers above 0 allowed.");
                    Console.ResetColor();
                    return null;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entered amount is not valid. Only integers allowed.");
                Console.ResetColor();
                return null;
            }

            Console.Write($"Date of transaction (yy-mm-dd): ");
            input = Console.ReadLine();
            input.Trim();
            if (DateTime.TryParse(input, out DateTime datevalue))
            {
                date = datevalue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a valid date");
                Console.ResetColor();
                return null;
            }

            return new Transaction(type, title, amount, date, account);
        }

        public static void EditTransaction()
        {
            //TODO
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This option is under construction");
            Console.ResetColor();
        }
        public static void SaveTransactions()
        {
            //TODO
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This option is under construction. Exiting program");
            Console.ResetColor();
        }

    }
}
