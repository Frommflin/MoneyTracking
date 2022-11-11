
namespace MoneyTracking
{
    internal class Methods
    {
        public static void ShowMenu()
        {
            string[] menu = { "Show transactions", "Add new transaction", "Edit transaction", "Save and exit" };
            
            Console.WriteLine("Menu: Type the number for the desired option to move on");
            Console.WriteLine("****************");
            
            for (int i = 1; i < 5; i++)
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(i);
                Console.ResetColor();
                Console.WriteLine($") {menu[i-1]}");
            }
        }
    }
}
