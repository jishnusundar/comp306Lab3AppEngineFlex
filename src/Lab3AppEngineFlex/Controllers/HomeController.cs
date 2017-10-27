using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab3AppEngineFlex.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string firstName, string lastName, string city, string postalCode,
            string phone, string province, string[] item, string type, string comments)
        {
            string connectionString = "Data Source=35.196.185.228,1433;Initial Catalog=comp306_hotel;Integrated Security=False;User ID=jishnusundar423;Password=password1234;MultipleActiveResultSets=True";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    string items = string.Join(",", item);
                    bool pickup = false;
                    pickup = type == "Pickup" ? true : false;
                    string query = "INSERT INTO orders (firstName,lastName,city,postalCode,phone,province,items,pickup,comments) VALUES (@fName,@lName,@city,@pCode,@phone,@province,@items,@pickup,@comments)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fName", firstName);
                        cmd.Parameters.AddWithValue("@lName", lastName);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@pCode", postalCode);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@province", province);
                        cmd.Parameters.AddWithValue("@items", items);
                        cmd.Parameters.AddWithValue("@pickup", pickup);
                        cmd.Parameters.AddWithValue("@comments", comments);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }


                return View();
            }


        }

        [HttpPost]
        public IActionResult checkUser(string firstName, string lastName)
        {
            string connectionString = "Data Source=35.196.185.228,1433;Initial Catalog=comp306_hotel;Integrated Security=False;User ID=jishnusundar423;Password=password1234;MultipleActiveResultSets=True";

            string query = "SELECT * FROM orders";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                TempData["userFound"] = null;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string fName = (reader["firstName"].ToString()).Trim();
                        string lName = (reader["lastName"].ToString()).Trim();
                        if (fName == firstName && lName == lastName)
                        {
                            string city = (reader["city"].ToString()).Trim();
                            string postalCode = (reader["postalCode"].ToString()).Trim();
                            string phone = (reader["phone"].ToString()).Trim();
                            string province = (reader["province"].ToString()).Trim();
                            string items = (reader["items"].ToString()).Trim();
                            bool pickup = (bool)reader["pickup"] ? true : false;
                            string comments = (reader["comments"].ToString()).Trim();

                            TempData["userFound"] = "Found";

                            TempData["firstName"] = fName;
                            TempData["lastName"] = lName;
                            TempData["city"] = city;
                            TempData["postalCode"] = postalCode;
                            TempData["phone"] = phone;
                            TempData["province"] = province;
                            TempData["items"] = items;
                            TempData["pickup"] = pickup == true ? "True" : "False";
                            TempData["comments"] = comments;


                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (TempData["userFound"] == null)
                    {
                        TempData["userFound"] = "Not Found";
                        TempData["firstName"] = firstName;
                        TempData["lastName"] = lastName;
                    }
                }

                else
                {
                    TempData["userFound"] = "Not Found";
                    TempData["firstName"] = firstName;
                    TempData["lastName"] = lastName;
                    Console.WriteLine("No rows found.");
                }
                connection.Close();
            }
            return RedirectToAction("Index");

        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
