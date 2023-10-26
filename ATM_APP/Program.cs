using System.Linq.Expressions;

namespace ATM_APP
{
    internal class Program
    {
        private static List<Users> users = new List<Users>();
        private static Users currentUser;
        static void Main(string[] args)
        {
            int loginTimes = 0;
            LoadUsersFromFile();

            while (loginTimes < 3)
            {
                Console.Clear();
                Console.WriteLine("===== Sveiki prisijunge i bankomata ======");
                Console.WriteLine("Iveskit korteles numeri: ");

                string cardId = Console.ReadLine();

                Console.WriteLine("Iveskit PIN: ");
                string pinCode = Console.ReadLine();

                string choise = "";

                if (Login(cardId, pinCode))
                {                   
                    while (choise != "4")
                    {
                        Console.Clear();
                        DisplayOption();
                        choise = Console.ReadLine();
                        if (choise == "4")
                        {
                            Console.WriteLine("=== Jus sekmingai atsijungete ===");
                            return;
                        }

                        switch (choise)
                        {
                            case "1":
                                ShowBalance();
                                break;
                            case "2":
                                ShowLastTransaction();
                                break;
                            case "3":
                                WithdrawMoney();
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("");
                }
                loginTimes++;
                int leftTimes = 3;
                leftTimes -= loginTimes;
                if (leftTimes > 0)
                {
                    Console.WriteLine($"Jums liko {leftTimes} kartai!");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Jus esate UZBLOKUOTAS, 3 kartus ivedete blogus prisijungimo duomenis");
                }
            }            
        }
        private static void LoadUsersFromFile()
        {
            string filePath = "C:\\Users\\rstak\\source\\Savarnkiskas darbas\\ATM_APP\\ATM_APP\\BankCard.txt"; // Provide the actual path to your user file


            try
            {
                //Nuskaitom is failo formatu CardId|PIN|Balansas
                string[] readLines = File.ReadAllLines(filePath);

                foreach (var card in readLines)
                {
                    string[] userCard = card.Split('|');

                    Users user = new Users
                    {
                        CardId = userCard[0],
                        PIN = userCard[1],
                        Balance = double.Parse(userCard[2])
                    };

                    users.Add(user);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ivyko klaida nuskaitant faila!!");
            }
        }
        public static bool Login(string cardId, string pinCode)
        {
            Users user = users.FirstOrDefault(currentCard => currentCard.CardId == cardId && currentCard.PIN == pinCode);

            if (user != null)
            {
                currentUser = user;
                return true;
            }
            else
            {
                Console.WriteLine("Ivesti blogi prisijungimo duomenys");
            }
            return false;
        }
        public static void DisplayOption()
        {
            Console.WriteLine("Sveiki prisijunge!!!");
            Console.WriteLine("");
            Console.WriteLine("Pasirinkite veiksmą:");
            Console.WriteLine("1. Matyti turimus pinigus");
            Console.WriteLine("2. Peržiūrėti paskutines transakcijas - kolkas neveikia!");
            Console.WriteLine("3. Pinigų išsiėmimas");
            Console.WriteLine("4. Išeiti");
            Console.WriteLine("");
            Console.WriteLine($"Balansas: {currentUser.Balance} EUR");
        }
        public static void ShowBalance()
        {
            Console.WriteLine($"Turimi pinigai: {currentUser.Balance} EUR");            
        }
        public static void ShowLastTransaction()
        {
            Console.WriteLine($"Transaction under construction");            
        }
        public static void WithdrawMoney()
        {
            Console.WriteLine("Iveskit suma kuria norite isgryninti: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            if (amount < 0)
            {
                Console.WriteLine("Neigiama reiksme negalima");
                Thread.Sleep(2000);
                return;
            }

            if (amount <= 1000 && amount < currentUser.Balance)
            {
                Console.WriteLine($"Dabartinis balansas {currentUser.Balance} EUR");
                currentUser.Balance -= amount;
                Console.WriteLine($"Jus issiemete {amount} EUR, jusu saskaitoje liko: {currentUser.Balance} EUR");
            }
            else if (amount > currentUser.Balance)
            {
                Console.WriteLine($"Jums nepakanka lesu issimti norimai sumai, kadangi jusu balansas {currentUser.Balance}");
            }
            else
            {
                Console.WriteLine($"Bankomatas neisduoda daugiau negu 1000Eur");
            }
            Thread.Sleep(2000);
        }

    }
}