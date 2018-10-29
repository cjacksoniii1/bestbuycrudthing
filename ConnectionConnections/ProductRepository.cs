using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace doingconnectionstrings
{
    class ProductRepository
    {
        public ProductRepository()
        { 

#if DEBUG

        string jsonText = File.ReadAllText("appsettings.development.json");
        //string connStr = JObject.Parse(jsonText)["ConnectionStrings"]["DefaultConnection"].ToString();

#else
            string jsonText = File.ReadAllText("appsettings.release.json");
#endif
        
        string connStr = JObject.Parse(jsonText)["ConnectionStrings"]["DefaultConnection"].ToString();
        this.connStr = connStr;

        }

        private string connStr;

        public void AddProduct(Product prod)    //Create
        {
            MySqlConnection conn = new MySqlConnection(connStr);    //connects to MySql server

            using (conn)        //uses connection, closes when finished
            {
                conn.Open();        //opens connections

                MySqlCommand cmd = conn.CreateCommand();    //"makes the command"
                cmd.CommandText = $"INSERT INTO products (Name, Price, CatagoryID) Values (@n, @p, @cid)";  //command text w/parameters, n, p, cid
                cmd.Parameters.AddWithValue("n", prod.Name);    //enters parameter 'n' without injection
                cmd.Parameters.AddWithValue("p", prod.Price);   //enters parameter 'p' without injection
                cmd.Parameters.AddWithValue("cid", prod.CatID); //enters parameter 'cid' without injection
                cmd.ExecuteNonQuery();

            }

             /*   Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    CatID = id
                };

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO products (ProductID, Name, Price) VALUES ({product.Name} {product.CatID} {product.Price});";
            cmd.ExecuteNonQuery(); */

        }

        public Product GetProduct(int id)                           //Reads single product
        {
            MySqlConnection conn = new MySqlConnection(connStr);        //establishes connection

            using (conn)            //uses connection, closes when finished
            {
                conn.Open();        //opens connection
                        
                MySqlCommand cmd = conn.CreateCommand();        //creates command
                cmd.CommandText = "SELECT ProductID, Name, CatagoryID, Price FROM products WHERE ProductID =" + id;     //command text
                MySqlDataReader dataReader = cmd.ExecuteReader();       //declares 'dataReader' to actually read shit

                if (dataReader.Read())      //if entered parameter ID exists...
                {
                    Product product = new Product()     //creates new product and returns it
                    {
                        Name = dataReader["Name"].ToString(),
                        ID = (int)dataReader["ProductID"],
                        CatID = (int)dataReader["CatagoryID"],
                        Price = (decimal)dataReader["Price"]
                    };
                    return product;

                }
                else
                {
                    Console.WriteLine("No ID found");
                    return null;
                }
            }
        }

        public List<Product> GetAllProducts()                       //(also Read)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            List<Product> products = new List<Product>();

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price FROM products;";

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        ID = (int)dataReader["ProductID"],
                        Name = dataReader["Name"].ToString(),
                        Price = (decimal)dataReader["Price"]

                    };

                    products.Add(product);
                }
            }
                return products;
        }
        public int UpdateProduct(Product prod)                 //update
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET Name = @n, Price = @p, CategoryId = @cID " +
                                    "WHERE ProductId = @pID;";
                cmd.Parameters.AddWithValue("n", prod.Name);
                cmd.Parameters.AddWithValue("p", prod.Price);
                cmd.Parameters.AddWithValue("cID", prod.CatID);
                cmd.Parameters.AddWithValue("pID", prod.ID);
                return cmd.ExecuteNonQuery();
            }

        }
        public int DeleteProduct(int ID)             //delete
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductID = @id;";
                cmd.Parameters.AddWithValue("id", ID);
                return cmd.ExecuteNonQuery();
            }
        }
    }   
}
        

    
