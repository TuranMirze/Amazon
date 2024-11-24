using Amazon.Context;
using Amazon.Exceptions;
using Amazon.Models;

namespace Amazon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AppDbContext db = new AppDbContext();
            //User user = new User();
            //user.Name = "Test";
            //user.Surname = "Test";
            //user.Password = "asdads";
            //user.Username = user.Surname + user.Name;
            //db.users.Add(user);
            //db.SaveChanges();

            List<User> users = new List<User>();
            List<Product> products = new List<Product>();
            List<Basket> baskets = new List<Basket>();

            bool f = false;
            string username;
            string password;
            int user_id = 0;
            do
            {
                Console.WriteLine("1.Login");
                Console.WriteLine("2.Register");
                Console.WriteLine("0.Exit");
                string choose = Console.ReadLine();

                switch (choose)
                {

                    case "1":
                        try
                        {
                            using (AppDbContext sql = new AppDbContext())
                            {
                                Console.WriteLine("Enter Username");
                                username = Console.ReadLine();
                                bool password_check = false;
                                do
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("Enter Password");
                                    password = Console.ReadLine();

                                    if (password.Length > 6 && password.Length < 25)
                                    {
                                        password_check = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Password is not suitable");
                                        password_check = false;
                                    }
                                } while (!password_check);
                                Console.Clear();


                                users = sql.users.ToList();
                                bool login_check = false;


                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (username == users[i].Username && password == users[i].Password)
                                    {

                                        login_check = true;
                                        user_id = users[i].Id;
                                    }
                                }
                                if (login_check == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Successfully logged in");
                                    bool mehsul = false;
                                    do
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine("1.View Products");
                                        Console.WriteLine("2.View Basket");
                                        Console.WriteLine("3.Exit from Account");
                                        string choose1 = Console.ReadLine();

                                        switch (choose1)
                                        {
                                            case "1":
                                                products = sql.products.ToList();
                                                foreach (var product in products)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine();
                                                    Console.WriteLine($"Product ID : {product.Id}, Product Name : {product.Name}, Product Price : {product.Price}");
                                                    Console.WriteLine();
                                                }
                                                break;
                                                bool mehsul2 = false;
                                                do
                                                {
                                                    Console.WriteLine("1.Add Product    2. Delete Product    0. Exit");
                                                    string choose2 = Console.ReadLine();

                                                    switch (choose2)
                                                    {
                                                        case "0":
                                                            mehsul2 = true;
                                                            break;
                                                        case "1":
                                                            bool isProductAdd = false;
                                                            int productAddId = 0;
                                                            do
                                                            {
                                                                try
                                                                {
                                                                    Console.WriteLine("Enter ProductId for add product:");
                                                                    if (int.TryParse(Console.ReadLine(), out productAddId))
                                                                    {
                                                                        var product = sql.products.FirstOrDefault(p => p.Id == productAddId);
                                                                        if (products != null)
                                                                        {
                                                                            isProductAdd = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new ProductNotFoundException("Product is not found!!");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FormatException("Enter the number!!");
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    Console.WriteLine(ex.Message);
                                                                }
                                                            } while (isProductAdd);

                                                            sql.SaveChanges();
                                                            Console.Clear();
                                                            Console.WriteLine("Product successfully added!");
                                                            break;
                                                        case "2":
                                                            bool isProductRemove = false;
                                                            int productRemoveId = 0;
                                                            do
                                                            {
                                                                try
                                                                {
                                                                    Console.WriteLine("Enter ProductId for remove product:");
                                                                    if (int.TryParse(Console.ReadLine(), out productRemoveId))
                                                                    {
                                                                        var basketItem = sql.baskets.FirstOrDefault(b => b.ProductId == productRemoveId && b.UsersId == user_id);
                                                                        if (basketItem != null)
                                                                        {
                                                                            isProductRemove = true;
                                                                            sql.baskets.Remove(basketItem);
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new ProductNotFoundException("Product is not found!!");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FormatException("Enter the number!!");
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Console.WriteLine(e.Message);
                                                                }
                                                            } while (!isProductRemove);

                                                            sql.SaveChanges();
                                                            Console.Clear();
                                                            Console.WriteLine("Product successfully removed!");
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                } while (!mehsul2);
                                                break;
                                            case "2":
                                                baskets = sql.baskets.ToList();
                                                Console.WriteLine(user_id);
                                                Console.WriteLine();
                                                foreach (var basket in baskets)
                                                {
                                                    if (basket.UsersId == user_id)
                                                    {
                                                        var product = sql.products.FirstOrDefault(p => p.Id == basket.ProductId);
                                                        if (product != null)
                                                        {
                                                            Console.WriteLine($"Product Name: {product.Name}");
                                                        }
                                                    }
                                                }
                                                break;
                                            case "3":
                                                mehsul = true;
                                                break;
                                            default:
                                                break;
                                        }

                                    } while (!mehsul);
                                }

                                else
                                    throw new UserNotFoundException("Please, enter username and password correctly!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "2":
                        using (AppDbContext sql = new AppDbContext())
                        {
                            bool name_check = false;
                            do
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Enter Name:");
                                string regname = Console.ReadLine();
                                if (regname.Length < 20 && regname.Length > 2)
                                {
                                    name_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Name is not suitable!");
                                    name_check = false;
                                }
                            } while (!name_check);

                            bool surname_check = false;
                            do
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Enter Surname:");
                                string regsurname = Console.ReadLine();
                                if (regsurname.Length < 20 && regsurname.Length > 2)
                                {
                                    surname_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Surname is not suitable!");
                                    surname_check = false;
                                }
                            } while (!surname_check);

                            bool username_check = false;
                            do
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Username daxil edin:");
                                string regusername = Console.ReadLine();
                                if (regusername.Length <= 20 && regusername.Length >= 3)
                                {
                                    username_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Username parametrleri uygun deyil!");
                                    username_check = false;
                                }
                            } while (!username_check);


                            bool password_check = false;
                            do
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Password daxil edin:");
                                string regpassword = Console.ReadLine();
                                if (regpassword.Length >= 8)
                                {
                                    password_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Password parametrleri uygun deyil!");
                                    password_check = false;
                                }
                            } while (!password_check);
                            Console.Clear();
                        }                           
                        break;
                    case "0":
                        return;
                    default:
                        break;
                }
            } while (!f);
        }
    }
}
