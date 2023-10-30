using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_APP
{
    internal class Users
    {
        public string CardId { get; set; }
        public string PIN { get; set; }
        public double Balance { get; set; }
        public string FullName { get; set; }
        public int NumberOfWithdrawals { get; set; } = 0;
        public List<Tuple<double, DateTime>> Transcation { get; set; } = new List<Tuple<double, DateTime>>();
        

        public Users Login(string cardId, string pinCode, List<Users> users)
        {
            Users user = users.FirstOrDefault(currentCard => currentCard.CardId == cardId && currentCard.PIN == pinCode);

            if (user != null)
            {
                return user;
                
            }
            else
            {
                Console.WriteLine("Ivesti blogi prisijungimo duomenys");
                return null;
            }            
        }
        public void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine($"Pinigu likutis: {Balance} EUR");

            Console.WriteLine("");
            Console.WriteLine("Spauskite Enter, gryzti i Meniu");
            string end = Console.ReadLine();
        }
        public void WithdrawMoney()
        {
            Console.Clear();
            Console.WriteLine("Iveskit suma kuria norite isgryninti: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            if (amount < 0)
            {
                Console.WriteLine("Neigiama reiksme negalima");
                Thread.Sleep(2000);
                return;
            }
            if (NumberOfWithdrawals > 5)
            {
                Console.WriteLine("Virsytas nusiemimu skaicius per diena!!! Pinigu nusiimti nebegalite, bandykite rytoj");
                Thread.Sleep(2000);
                return;
            }

            if (amount <= 1000 && amount < Balance && NumberOfWithdrawals < 5)
            {
                Console.WriteLine($"Dabartinis balansas {Balance} EUR");
                Balance -= amount;
                Console.WriteLine($"Jus issiemete {amount} EUR, jusu saskaitoje liko: {Balance} EUR");
            }
            else if (amount > Balance)
            {
                Console.WriteLine($"Jums nepakanka lesu issimti norimai sumai, kadangi jusu balansas {Balance}");
            }
            else
            {
                Console.WriteLine($"Bankomatas neisduoda daugiau negu 1000Eur");
            }            

            DateTime transactionDateTime = DateTime.Now;
            Transcation.Add(new Tuple<double, DateTime>(amount, transactionDateTime));
            NumberOfWithdrawals++;

            Console.WriteLine("");
            Console.WriteLine("Spauskite Enter, gryzti i Meniu");
            string end = Console.ReadLine();
        }
        public void ShowLastTransaction()
        {            
            Console.Clear();
                if (Transcation.Any())
                {
                    Console.WriteLine($"Tranzakcijos kortelei: {CardId}");
                    foreach (var transacion in Transcation)
                    {                    
                        Console.WriteLine($"{FullName}, Operacija pinigu nusiemimas, Suma: {transacion.Item1} EUR, Data: {transacion.Item2}");
                    }
                }
                else
                {
                    Console.WriteLine($"Nerasta jokiu tranzakcju sia kortelei {CardId}");                    
                }
            Console.WriteLine("");
            Console.WriteLine("Spauskite Enter, gryzti i Meniu");
            string end = Console.ReadLine();
        }
    }
}
