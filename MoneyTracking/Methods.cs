
using System.Text;

namespace MoneyTracking
{
    internal class Methods
    {
        //directory = path to my local project repository
        private static string directory = @"C:\Users\Freak\OneDrive\Skrivbord\Skolshiet\C-Sharp .NET\Projects\MoneyTracking\TransactionsData\";
        private static string fileName = "transactions.text"; 

        internal static void CheckForExistingFile()
        {
            string path = $"{directory}{fileName}";

            if (!File.Exists(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(directory);

                    ShowMessage("Directory created and ready for saving files.", "Blue");
                }
            }
        }
        
        public static void LoadTransactions(List<Transaction> transactions, BankAccount account)
        {
            string path = $"{directory}{fileName}";
            if (File.Exists(path))
            {
                //Collect all saved data in a string array
                string[] transactionsString = File.ReadAllLines(path);

                //Looping through the array to re-create the transactions into the list
                for(int i = 0; i < transactionsString.Length; i++)
                {  
                    string[] split = transactionsString[i].Split(';');
                    int id = int.Parse(split[0].Substring(split[0].IndexOf(':') + 1));
                    string type = split[1].Substring(split[1].IndexOf(':') + 1);
                    string title = split[2].Substring(split[2].IndexOf(':') + 1);
                    int amount = int.Parse(split[3].Substring(split[3].IndexOf(':') + 1));
                    DateTime date = DateTime.Parse(split[4].Substring(split[4].IndexOf(':') + 1));

                    transactions.Add(new Transaction(id, type, title, amount, date, account));
                };
            }
        }
        
        public static void ShowMenu()
        {
            string[] menu = { "Show transactions", "Add new transaction", "Edit transaction", "Save and exit" };

            Console.WriteLine("Menu: Type the number for the desired option to move on");
            Console.WriteLine("**************************");
            for (int i = 0; i < menu.Length; i++)
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(i + 1);
                Console.ResetColor();
                Console.WriteLine($") {menu[i]}");
            }
            Console.WriteLine("**************************");
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Type".PadRight(10) + "Amount".PadRight(10) + "Transaction month".PadRight(20) + "Title");
            Console.WriteLine("-----------------------------------------------");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Id".PadRight(10) + "Type".PadRight(10) + "Amount".PadRight(10) + "Transaction month".PadRight(20) + "Title");
            Console.WriteLine("-----------------------------------------------------------");
            Console.ResetColor();
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction.Id.ToString().PadRight(10) + transaction.Type.PadRight(10) + transaction.Amount.ToString().PadRight(10) + transaction.Date.ToString("MMMM").PadRight(20) + transaction.Title);
            }
            Console.WriteLine();
        }
        
        public static void AddNewTransaction(List<Transaction> transactions, BankAccount account)
        {
            string input;
            string type = "";
            string title = "";
            int amount = 0;
            DateTime date = new DateTime(0001,01,01);

            ShowMessage("Follow instructions to add a new transaction. Enter 'Q' to exit", "Blue");
            do
            {
                InputLine($"Type of transaction (income/expense): ");
                input = Console.ReadLine();
                input.Trim().ToLower();
                if (input == "q")
                {
                    return;
                }
                else if (input == "income" || input == "expense")
                {
                    type = input;
                }
                else
                {
                    ShowMessage("Invalid transaction type.", "Red");
                }
            } while (type == "");
            
            do
            {
                InputLine($"Title of transaction: ");
                input = Console.ReadLine();
                input.Trim();
                if (input == "q")
                {
                    return;
                }
                else if (!String.IsNullOrEmpty(input))
                {
                    title = input;
                }
                else
                {
                    ShowMessage("Title cannot be empty.", "Red");
                }
            } while (title == "");
            
            do
            {
                InputLine($"Amount: ");
                input = Console.ReadLine();
                input.Trim();
                if (input == "q")
                {
                    return;
                }
                else if (int.TryParse(input, out int value))
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
                        ShowMessage("Invalid amount. Only integers above 0 allowed.", "Red");
                    }
                }
                else
                {
                    ShowMessage("Entered amount is not valid. Only integers allowed.", "Red");
                }
            } while (amount == 0);
            
            do
            {
                InputLine($"Date of transaction (yy-mm-dd): ");
                input = Console.ReadLine();
                input.Trim();
                if (input == "q")
                {
                    return;
                }
                else if (DateTime.TryParse(input, out DateTime datevalue))
                {
                    date = datevalue;
                }
                else
                {
                    ShowMessage("Not a valid date", "Red");
                }
            } while (date == new DateTime(0001, 01, 01));
            
            transactions.Add(new Transaction(type, title, amount, date, account));
        }

        public static void EditTransaction(List<Transaction> transactions)
        {
            string input;
            int id = 0;

            ShowMessage("What transaction would you like to edit? ('Q' to exit editing)", "Blue");
            do
            {
                InputLine("Transaction ID: ");
                input = Console.ReadLine();
                input.Trim();

                if(int.TryParse(input, out int value))
                {
                    foreach(var transaction in transactions)
                    {
                        if (value.Equals(transaction.Id))
                        {
                            id = value;
                            input = "q";
                            break;
                        }
                    }
                }
                else
                {
                    if(input != "q")
                    {
                        ShowMessage("Not a valid ID", "Red");
                    }
                    return;
                }
                
            } while (input.ToLower() != "q");

            do
            {
                InputLine("Do you wish to delete or edit this transacion? ");
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
                            ShowMessage("What field do you like to edit?", "Blue");
                            Console.WriteLine("(1) Amount");
                            Console.WriteLine("(2) Month");
                            Console.WriteLine("(3) Title");
                            InputLine("Edit field: ");
                            input = Console.ReadLine();
                            input.Trim();

                            if (input == "1") //edit amount
                            {
                                do
                                {
                                    InputLine($"New amount: ");
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
                                            ShowMessage("Invalid amount. Only integers above 0 allowed.", "Red");
                                        }
                                    }
                                    else
                                    {
                                        ShowMessage("Entered amount is not valid. Only integers allowed.", "Red");
                                    }
                                } while (input != "q");
                                
                            }
                            else if (input == "2") //edit date
                            {
                                do
                                {
                                    InputLine($"New date (yy-mm-dd): ");
                                    input = Console.ReadLine();
                                    input.Trim();
                                    if (DateTime.TryParse(input, out DateTime datevalue))
                                    {
                                        transaction.Date = datevalue;
                                        input = "q";
                                    }
                                    else
                                    {
                                        ShowMessage("Not a valid date", "Red");
                                    }
                                } while (input != "q");
                            }
                            else if (input == "3") //edit title
                            {
                                do
                                {
                                    InputLine($"New transaction title: ");
                                    input = Console.ReadLine();
                                    input.Trim();
                                    if (!String.IsNullOrEmpty(input))
                                    {
                                        transaction.Title = input;
                                        input = "q";
                                    }
                                    else
                                    {
                                        ShowMessage("Title cannot be empty.", "Red");
                                    }
                                } while (input != "q");
                            }
                            else
                            {
                                ShowMessage("Not a valid option", "Red");
                            }
                        } while (input.ToLower() != "q");
                    }
                    else
                    {
                        ShowMessage("Not a valid option", "Red");
                    }
                }
            } while (input != "q");
        }
        
        public static void SaveTransactions(List<Transaction> transactions)
        {
            string path = $"{directory}{fileName}";
            StringBuilder builder = new StringBuilder();
            foreach(Transaction transaction in transactions)
            {
                builder.Append($"id:{transaction.Id};");
                builder.Append($"type:{transaction.Type};");
                builder.Append($"title:{transaction.Title};");
                builder.Append($"amount:{transaction.Amount};");
                builder.Append($"date:{transaction.Date};");
                builder.Append(Environment.NewLine);
            }
            File.WriteAllText(path, builder.ToString());
        }

        public static string FilterList()
        {
            string input;
            string filter = "";

            ShowMessage("What transactions would you like to see?", "Blue");
            do
            {
                InputLine("Incomes or Expenses. Other inputs shows all transactions: ");
                input = Console.ReadLine();
                input.Trim().ToLower();

                if (input == "e" || input == "expense" || input == "expenses")
                {
                    filter = "expense";
                } 
                else if (input == "i" || input == "income" || input == "incomes")
                {
                    filter = "income";
                }
                else
                {
                    filter = "all";
                }
            } while (filter == "");

            return filter;
        }

        public static string[] SortList()
        {
            string input;
            string order = "";
            string field = "";

            ShowMessage("What field do you want to order the transactions by?", "Blue");
            Console.WriteLine("(1) Amount");
            Console.WriteLine("(2) Month");
            Console.WriteLine("(3) Title");
            do
            {
                InputLine("Order by field: ");
                input = Console.ReadLine();
                input.Trim();

                if (input == "1" || input == "2" || input == "3")
                {
                    field = input;
                }
                else
                {
                    ShowMessage("Invalid input.", "Red");
                }
            } while (field == "");

            ShowMessage("How do you want to sort the transactions?", "Blue");
            do
            {
                InputLine("'A' for ascending or 'D' for descending: ");
                input = Console.ReadLine();
                input.Trim().ToLower();

                if (input == "d" || input == "des" || input =="descending")
                {
                    order = "d";
                }
                else if (input == "a" || input == "asc" || input == "ascending")
                {
                    order = "a";
                }
                else
                {
                    ShowMessage("Invalid input.", "Red");
                }
            } while (order == "");

            string[] sorted = { field, order };

            return sorted;
        }

        public static void ShowMessage(string message, string color) //Showing line of message/instruction in color 
        {
            switch(color)
            {
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue; 
                    break;
                case "Cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default: 
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void InputLine(string message) //Showing line in color before place for input
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
