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
        public List<double> Transcation { get; set; } = new List<double>();
        public DateTime TransactionDateTIme { get; set; }

        public void ShowLastTransaction()
        {            
                if (Transcation.Any())
                {
                    Console.WriteLine($"Tranzakcijos kortelei: {CardId}");
                    foreach (var transacion in Transcation)
                    {
                        Console.WriteLine($"Operacija pinigu nusiemimas, Suma: {transacion} EUR");
                    }
                }
                else
                {
                    Console.WriteLine($"Nerasta jokiu tranzakcju sia kortelei {CardId}");                    
                }
            Thread.Sleep(3000);
        }
    }
}
