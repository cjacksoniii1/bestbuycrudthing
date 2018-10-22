using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace doingconnectionstrings
{
    class Program
    {
        private static ProductRepository repo;

        static void Main(string[] args)
        {
            StartProgram();
        }
        static void StartProgram()
        {
            Console.WriteLine("Building Repository...");
            repo = new ProductRepository();
            Console.WriteLine("Listing Products...");
            List<Product> products = repo.GetAllProducts();

            Console.WriteLine("Program Ready:");
            Console.WriteLine("Please Enter a valid command or type \"end\"");
            Console.WriteLine("*Create* | *Update* | *Read* | *Delete* ");

            string response = "";
            while (true)
            {
                response = Console.ReadLine();

                if (response.ToLower() == "create")
                    CreateLogic();
                if (response.ToLower() == "update")
                    UpdateLogic();        //**TODO** make UpdateLog function
                if (response.ToLower() == "read")
                    ReadLogic();          //**TODO** make ReadLog function
                if (response.ToLower() == "delete")
                    DeleteLogic();        //**TODO** make DeleteLog function
                if (response.ToLower() == "end")
                    break;
                else
                {
                    Console.WriteLine("Please give a valid command");
                    Console.WriteLine("*Create* | *Update* | *Read* | *Delete* ");
                }
            }
        }
        static void CreateLogic()
        {
            Console.WriteLine("Creating new entry...");

            Product prod = new Product()
            {
                Name = GetString(),
                Price = GetPrice(),
                CatID = GetInteger()
            };

            Console.WriteLine("Adding new product...");
            repo.AddProduct(prod);
            Console.WriteLine("Succses!");

        }
        static void UpdateLogic()
        {
        }
        static void ReadLogic()
        {
        }
        static void DeleteLogic()
        { }
        private static string GetString()
        {
            string answer = Console.ReadLine();
            if (answer.Length < 3)
            {
                Console.WriteLine("Please enter a name that is at least 3 characters long");
                answer = GetString();
            }
            return answer;
        }
        private static decimal GetPrice()
        {
            string answer = Console.ReadLine();
            decimal number = 0;
            if (Decimal.TryParse(answer, out number))
                return number;

            Console.WriteLine("Please enter valid input (stored as decimal value)");
            return GetPrice();
        }
        private static int GetInteger()
        {
            string answer = Console.ReadLine();
            int number = 0;
            if (Int32.TryParse(answer, out number))
                return number;

            Console.WriteLine("Please enter a non-negative, non-zero integer.");
            return GetInteger();
        }
               
    }
}
