using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.WriteLine("X---------------------------X");
            Console.WriteLine("X Choose an option:         X");
            Console.WriteLine("X 1) View accounts          X");
            Console.WriteLine("X 2) View account by number X");
            Console.WriteLine("X 3) Exit                   X");
            Console.WriteLine("X Select an option:         X");
            Console.Write("X---------------------------X\r\n");

            switch (Console.ReadLine())
            {
                case "1":
                    ViewAccounts();
                    return true;
                case "2":
                    ViewByID();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

        private static void ViewAccounts()
        {
            Console.Clear();
            var accounts = ReadAccounts();
            Console.WriteLine("<--------------------------------->");
            Console.WriteLine("< Number | Balance | Label | Owner>");
            Console.WriteLine("<--------------------------------->");

            foreach (var account in accounts)
            {
                if (account.Label == "Secret")
                {
                    Console.WriteLine("<   " + account.Number + "   |   " + account.Balance + "    |" + account.Label + " |  " + account.Owner + "  >");
                }
                else if (account.Label == "Savings")
                {
                    Console.WriteLine("< " + account.Number + " |   " + account.Balance + "    |" + account.Label + "|  " + account.Owner + "  >");
                }
                else if (account.Label == "Salary")
                {
                    Console.WriteLine("< " + account.Number + " |   " + account.Balance + "    |" + account.Label + " |  " + account.Owner + "  >");
                }
                else if (account.Label == "Travel")
                {
                    Console.WriteLine("< " + account.Number + " |   " + account.Balance + "    |" + account.Label + " |  " + account.Owner + "  >");
                }
                Console.WriteLine("<--------------------------------->");
            }
            Console.Write("\r\nPress enter to return to main menu");
            Console.ReadLine();
        }

        private static void ViewByID()
        {
            var accounts = ReadAccounts();
            Console.Clear();
            Console.WriteLine("Enter an ID/Number: ");
            var ownerId = Console.ReadLine();

            foreach (var account in accounts)
            {
                if (account.Number == Int32.Parse(ownerId))
                {
                    if (ownerId.Length < 5)
                    {
                        Console.WriteLine("<---------------------------------->");
                        Console.WriteLine("< Number | Balance | Label | Owner >");
                        Console.WriteLine("<---------------------------------->");
                        Console.WriteLine("<   " + account.Number + "   |   " + account.Balance + "    |" + account.Label + " |  " + account.Owner + "   >");
                        Console.WriteLine("<---------------------------------->");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("<---------------------------------->");
                        Console.WriteLine("< Number | Balance | Label | Owner >");
                        Console.WriteLine("<---------------------------------->");
                        Console.WriteLine("< " + account.Number + " |   " + account.Balance + "    |" + account.Label + " |  " + account.Owner + "   >");
                        Console.WriteLine("<---------------------------------->");
                        break;
                    }
                }
            }
            Console.Write("\r\nPress enter to return to main menu");
            Console.ReadLine();
        }

        static IEnumerable<Account> ReadAccounts()
        {
            String file = "../data/account.json";

            using (StreamReader r = new StreamReader(file))
            {
                string data = r.ReadToEnd();
                var json = JsonSerializer.Deserialize<Account[]>(
                    data,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
                return json;
            }
        }

    }
    public class Account
    {
        public int Number { get; set; }
        public int Balance { get; set; }
        public string Label { get; set; }
        public int Owner { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Account>(this);
        }
    }
}
