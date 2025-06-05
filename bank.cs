using System;
using System.Threading;

namespace ConsoleBank
{
    class BankAccount
    {
        public string Username
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public double Balance
        {
            get;
            private set;
        }

        private bool IsLoggedIn
        {
            get;
            set;
        }

        public BankAccount(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Balance = 0;
            this.IsLoggedIn = false;
        }

        public static bool Login(BankAccount account, string username, string password)
        {
            if ( account.Username == username && account.Password == password )
            {
                account.IsLoggedIn = true;
            }

            return account.IsLoggedIn;
        }

        public void Deposit(double amount)
        {
            if (this.IsLoggedIn == true)
            {
                if (amount < 0)
                {
                    Console.WriteLine("Cannot deposit negative amount!");
                }
                else
                {
                    if (amount > 100000)
                    {
                        Console.WriteLine("Cannot deposit +$100K into bank account!");
                    }
                    else
                    {
                        this.Balance += amount;
                        Console.WriteLine($"Successfuly deposited ${amount} into bank account!");
                    }
                }
            }
            else
            {
                Console.WriteLine("You haven't logged into this account!");
            }
        }

        public void Withdraw(double amount)
        {
            if (this.IsLoggedIn == true)
            {
                if (amount > this.Balance)
                {
                    Console.WriteLine("Insufficient funds!");
                }
                else
                {
                    if (amount < 0)
                    {
                        Console.WriteLine("Cannot withdraw negative amount!");
                    }
                    else
                    {
                        this.Balance -= amount;
                        Console.WriteLine($"Successfuly withdrawed ${amount} from bank account!");
                    }
                }
            }
            else
            {
                Console.WriteLine("You haven't logged into this account!");
            }
        }
    }

    class Program
    {
        static BankAccount account = new BankAccount("Username", "Password");
        
        static void Main(string[] args)
        {
            Loop();
        }

        static void Loop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            bool is_success;

            do
            {
                is_success = Login();

                if (is_success == true)
                {
                    MainLoop();
                }
            } while (is_success == false);

            Console.ReadKey();
        }

        static bool Login()
        {
            Console.WriteLine("---- LOGIN ----");
            string username = String.Empty;
            string password = String.Empty;

            do
            {
                Console.Write("Username: ");
                username = Console.ReadLine();

            } while (username == String.Empty);

            do
            {
                Console.Write("Password: ");
                password = Console.ReadLine();

            } while (password == String.Empty);

            if ( BankAccount.Login(account, username, password) )
            {
                Console.WriteLine("Logged in!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid login information!");
                return false;
            }

        }

        static void MainLoop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("******** CONSOLE BANK ********");
            Console.Write("1. Deposit\n2. Withdraw\n3. Check Balance\n4. Exit\nChoose an option: ");
            bool is_success = int.TryParse(Console.ReadLine(), out int result);

            if ( is_success )
            {
                switch(result)
                {
                    case 1:
                        Deposit();
                        break;
                    case 2:
                        Withdraw();
                        break;
                    case 3:
                        CheckBalance();
                        break;
                    case 4:
                        account.Logout();
                        break;
                    default:
                        Console.WriteLine("Enter a valid option!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Enter a valid number as integer!");
                MainLoop();
            }
        }

        static void Deposit()
        {
            Console.WriteLine("---- Deposit ----");
            Console.Write($"Enter amount: ");
            bool is_success = double.TryParse(Console.ReadLine(), out double amount);

            if ( is_success )
            {
                account.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Enter a valid number!");
                Deposit();
            }

            Thread.Sleep(2500);

            MainLoop();
        }

        static void Withdraw()
        {
            Console.WriteLine("---- Withdraw ----");
            Console.Write($"Enter amount: ");
            bool is_success = double.TryParse(Console.ReadLine(), out double amount);

            if (is_success)
            {
                account.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Enter a valid number!");
                Deposit();
            }

            Thread.Sleep(2500);

            MainLoop();
        }

        static void CheckBalance()
        {
            Console.WriteLine("---- Balance ----");
            Console.WriteLine($"Bank Balance: ${Math.Round(account.Balance, 2)}");

            Thread.Sleep(2500);

            MainLoop();
        }
    }
}
