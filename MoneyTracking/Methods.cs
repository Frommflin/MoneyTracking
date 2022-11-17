
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
                Console.Write(i + 1);
                Console.ResetColor();
                Console.WriteLine($") {menu[i]}");
            }
        }

        public static void ShowTransactions(List<Transaction> transactions,string filter, string[] sortBy)
        {
            //TODO
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This option is under construction");
            Console.ResetColor();

            List<Transaction> orderedList;
            if (sortBy[0] == "1") // Sorting by Amount
            {
                if (sortBy[1] == "a")
                {
                    orderedList = transactions.OrderBy(t => t.Amount).ToList();
                }
                else
                {
                    orderedList = transactions.OrderByDescending(t => t.Amount).ToList();
                }
            }
            else if (sortBy[0] == "2") // Sorting by Month
            {
                if (sortBy[1] == "a")
                {
                    orderedList = transactions.OrderBy(t => t.Date).ToList();
                }
                else
                {
                    orderedList = transactions.OrderByDescending(t => t.Date).ToList();
                }
            }
            else  // Sorting by Title
            {
                if (sortBy[1] == "a")
                {
                    orderedList = transactions.OrderBy(t => t.Title).ToList();
                }
                else
                {
                    orderedList = transactions.OrderByDescending(t => t.Title).ToList();
                }
            }

            Console.WriteLine("Type".PadRight(10) + "Amount".PadRight(10) + "Transaction month".PadRight(20) + "Title");
            Console.WriteLine("-----------------------------------------------");
            foreach (var transaction in orderedList)
            {
                if (filter == transaction.Type) //showing only income or expense
                {
                    Console.WriteLine(transaction.Type.PadRight(10) + transaction.Amount.ToString().PadRight(10) + transaction.Date.ToString("MMMM").PadRight(20) + transaction.Title);
                }
                else if (filter == "all") // showing all items in list
                {
                    Console.WriteLine(transaction.Type.PadRight(10) + transaction.Amount.ToString().PadRight(10) + transaction.Date.ToString("MMMM").PadRight(20) + transaction.Title);
                }
            }
        }

        public static void ShowTransactionsId(List<Transaction> transactions)
        {
            Console.WriteLine("Id".PadRight(10) + "Type".PadRight(10) + "Amount".PadRight(10) + "Transaction month".PadRight(20) + "Title");
            Console.WriteLine("-----------------------------------------------------------");
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction.Id.ToString().PadRight(10) + transaction.Type.PadRight(10) + transaction.Amount.ToString().PadRight(10) + transaction.Date.ToString("MMMM").PadRight(20) + transaction.Title);
            }
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

        public static string FilterList()
        {
            string input;
            string filter = "";

            Console.WriteLine("What transactions would you like to see?");
            do
            {
                Console.Write("'I' for incomes, 'E' for expenses or 'A' for all transactions: ");
                input = Console.ReadLine();
                input.Trim().ToLower();

                if (input == "a")
                {
                    filter = "all";
                } 
                else if (input == "e")
                {
                    filter = "expense";
                } 
                else if (input == "i")
                {
                    filter = "income";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                }
            } while (filter == "");

            return filter;
        }

        public static string[] SortList()
        {
            string input;
            string order = "";
            string field = "";

            Console.WriteLine("What field do you want to order the data by?");
            Console.WriteLine("(1) Amount");
            Console.WriteLine("(2) Month");
            Console.WriteLine("(3) Title");
            do
            {
                Console.Write("Order by field number: ");
                input = Console.ReadLine();
                input.Trim();

                if (input == "1" || input == "2" || input == "3")
                {
                    field = input;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                }
            } while (field == "");

            Console.WriteLine("How do you want to sort the transactions?");
            do
            {
                Console.Write("'A' for ascending or 'D' for descending: ");
                input = Console.ReadLine();
                input.Trim().ToLower();

                if (input == "d" || input == "a")
                {
                    order = input;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                }
            } while (order == "");

            string[] sorted = { field, order };

            return sorted;
        }
    }
}
