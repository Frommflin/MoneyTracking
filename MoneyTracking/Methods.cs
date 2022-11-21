
namespace MoneyTracking
{
    internal class Methods
    {
        //directory = path to my local project repository
        private static string directory = @"C:\Users\Freak\OneDrive\Skrivbord\Skolshiet\C-Sharp .NET\Projects\MoneyTracking\TransactionsData";
        private static string fileName = "transactions.text"; 

        internal static void CheckForExistingFile()
        {
            string path = $"{directory}{fileName}";

            if (File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("An existing file with transactionsdata is found.");
                Console.ResetColor();
            }
            else
            {
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(directory);
                    
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Directory ready for saving files."); 
                    Console.ResetColor();
                }
            }
        }
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
                ShowError("Invalid transaction type.");
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
                ShowError("Title cannot be empty.");
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
                    ShowError("Invalid amount. Only integers above 0 allowed.");
                    return null;
                }
            }
            else
            {
                ShowError("Entered amount is not valid. Only integers allowed.");
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
                ShowError("Not a valid date");
                return null;
            }

            return new Transaction(type, title, amount, date, account);
        }

        public static void EditTransaction(ref List<Transaction> transactions)
        {

            string input;
            int id = 0;

            Console.WriteLine("What transaction would you like to edit? ('Q' to exit editing)");
            do
            {
                Console.Write("Transaction ID: ");
                input = Console.ReadLine();
                input.Trim();

                if(int.TryParse(input, out int value))
                {
                    foreach(var transaction in transactions)
                    {
                        if (value.Equals(transaction.Id))
                        {
                            id = value;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"ID: {id}");
                            Console.ResetColor();
                            input = "q";
                            break;
                        }
                    }
                }
                else
                {
                    ShowError("Not a valid ID");
                    return;
                }
                
            } while (input.ToLower() != "q");

            do
            {
                Console.Write("Do you wish to delete or edit this transacion? ");
                input = Console.ReadLine();
                input.Trim().ToLower();

                List<Transaction> item = transactions.Where(x => x.Id == id).ToList(); //storing the transaction with given id
                foreach (Transaction transaction in item)
                {
                    if (input == "delete")
                    {
                        int money = -transaction.Amount;
                        transaction.BankAccount.UpdateBalance(money);
                        transactions.Remove(transaction);
                        return;
                    }
                    else if (input == "edit")
                    {
                        do
                        {
                            Console.WriteLine("What field do you like to edit?");
                            Console.WriteLine("(1) Amount");
                            Console.WriteLine("(2) Month");
                            Console.WriteLine("(3) Title");
                            Console.Write("Edit field: ");
                            input = Console.ReadLine();
                            input.Trim();

                            if (input == "1") //edit amount
                            {
                                do
                                {
                                    Console.Write($"New amount: ");
                                    input = Console.ReadLine();
                                    input.Trim();
                                    if (int.TryParse(input, out int value))
                                    {
                                        if (value > 0)
                                        {
                                            int returnMoney = -transaction.Amount;
                                            transaction.BankAccount.UpdateBalance(returnMoney);

                                            if (transaction.Type == "expense")
                                            {
                                                transaction.Amount = -value; // stores negative value
                                            }
                                            else // type == income
                                            {
                                                transaction.Amount = value;
                                            }
                                            transaction.BankAccount.UpdateBalance(transaction.Amount);
                                            input = "q";
                                        }
                                        else
                                        {
                                            ShowError("Invalid amount. Only integers above 0 allowed.");
                                        }
                                    }
                                    else
                                    {
                                        ShowError("Entered amount is not valid. Only integers allowed.");
                                    }
                                }while(input != "q");
                                
                            }
                            else if (input == "2") //edit date
                            {
                                do
                                {
                                    Console.Write($"New date (yy-mm-dd): ");
                                    input = Console.ReadLine();
                                    input.Trim();
                                    if (DateTime.TryParse(input, out DateTime datevalue))
                                    {
                                        transaction.Date = datevalue;
                                        input = "q";
                                    }
                                    else
                                    {
                                        ShowError("Not a valid date");
                                    }
                                }while(input != "q");
                            }
                            else if (input == "3") //edit title
                            {
                                do
                                {
                                    Console.Write($"New transaction title: ");
                                    input = Console.ReadLine();
                                    input.Trim();
                                    if (!String.IsNullOrEmpty(input))
                                    {
                                        transaction.Title = input;
                                        input = "q";
                                    }
                                    else
                                    {
                                        ShowError("Title cannot be empty.");
                                    }
                                }while(input != "q");
                            }
                            else
                            {
                                ShowError("Not a valid option");
                            }
                        } while (input.ToLower() != "q");
                    }
                    else
                    {
                        ShowError("Not a valid option");
                    }
                }
            }while(input != "q");
            

        }
        
        public static void SaveTransactions()
        {
            //TODO
            ShowError("This option is under construction. Exiting program");
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
                    ShowError("Invalid input.");
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
                    ShowError("Invalid input.");
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
                    ShowError("Invalid input.");
                }
            } while (order == "");

            string[] sorted = { field, order };

            return sorted;
        }

        private static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
