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
                    UpdateLogic();       
                if (response.ToLower() == "read")
                    ReadLogic();          
                if (response.ToLower() == "delete")
                    DeleteLogic();        n
                if (response.ToLower() == "end")
                    break;
                else
                {
                    Console.WriteLine("Please give a valid command or type \"end\"");
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
            Product prod = new Product();

            Console.WriteLine("Which product do you wish to update?");
            prod.ID = GetInteger();
            Console.WriteLine("Give me a name");
            prod.Name = GetString();
            Console.WriteLine("Give me a price");
            prod.Price = GetPrice();
            Console.WriteLine("Give me a Catagory ID");
            prod.CatID = GetInteger();

            int result = repo.UpdateProduct(prod);
            if (result > 0)
            {
                Console.WriteLine("Update Succsesful");
            }
            else
            {
                Console.WriteLine("Update Unsuccsesful, Product ID does not exist");
            }
        }
        static void ReadLogic()
        {
            Console.WriteLine("What is the Product ID that you are looking for?");
            Product prod = new Product();
            prod = repo.GetProduct(GetInteger());
            Console.WriteLine($" {prod.ID}  |  {prod.Name}  |  {prod.Price}  |  {prod.CatID}");

        }
        /*          **TODO** implement both functions
        static void PrintAllProducts(List<Product> prod)
        {

        }
        static void PrintOneProduct()
        {
        

        }
        */
        static void DeleteLogic()
        {
            Console.WriteLine("Give me the product ID that you wish to delete");
            int ID = GetInteger();
            int result = repo.DeleteProduct(ID);
            if (result > 0)
            {
                Console.WriteLine("Record Deleted");
            }
            else
                Console.WriteLine("Record not Deleted: ID not found");
        }
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
