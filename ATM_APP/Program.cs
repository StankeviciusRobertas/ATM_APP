using System.Linq.Expressions;
using System.Transactions;

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

                Console.WriteLine("Iveskite PIN: ");
                string pinCode = Console.ReadLine();

                string choise = "";
                currentUser = new Users();
                Users loggedInUser = currentUser.Login(cardId, pinCode, users);

                if (loggedInUser != null)
                {                   
                    currentUser = loggedInUser;
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
                                currentUser.ShowBalance();
                                break;
                            case "2":
                                currentUser.ShowLastTransaction();
                                break;
                            case "3":
                                currentUser.WithdrawMoney();
                                break;
                            default:
                                Console.WriteLine($"Netesingas pasirinkimas. Bandykite dar karta!");
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
                    Thread.Sleep(3000);
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
                        Balance = double.Parse(userCard[2]),
                        FullName = userCard[3]
                    };

                    users.Add(user);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ivyko klaida nuskaitant faila!!");
            }
        }        
        public static void DisplayOption()
        {            
            Console.WriteLine("Sveiki prisijunge!!!");
            Console.WriteLine("");
            Console.WriteLine($"Prisijunges: {currentUser.FullName}");
            Console.WriteLine($"Balansas: {currentUser.Balance} EUR");
            Console.WriteLine("");
            Console.WriteLine("Pasirinkite veiksmą:");
            Console.WriteLine("1. Balansas");
            Console.WriteLine("2. Peržiūrėti paskutines transakcijas");
            Console.WriteLine("3. Pinigų išsiėmimas");
            Console.WriteLine("4. Išeiti");
            Console.WriteLine("");            
        } 
    }
}