using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterKata
{
    class Program
    {

        static void Main(string[] args)
        {
            decimal input = GetDecimal();
            CashRegister cr = new CashRegister();
            Console.WriteLine(cr.ProcessChange(input));
            Console.WriteLine(cr.ProcessRandomChange(input));
            Console.ReadKey();
            Console.WriteLine(cr.ProcessRandomChange(input));
            Console.ReadKey();
            Console.WriteLine(cr.ProcessRandomChange(input));
            Console.ReadKey();
            Console.WriteLine(cr.ProcessRandomChange(input));
            Console.ReadKey();

        }



        
        /// <summary>
        /// GetDecimal()
        /// grabs a non negative integer from the User through the Console
        /// </summary>
        /// <param name="prompt">Takes a String to display to User before readline</param>
        /// <returns>non Negative integer</returns>
        static decimal GetDecimal(string prompt = "Enter a Decimal:")
        {
            decimal result = -1;
            string response = "";
            do
            {
                Console.Write(prompt);
                response = Console.ReadLine();

            } while (!decimal.TryParse(response, out result));
            return result;
        }
    }


    class CashRegister
    {
        private Currency[] myCurrencies;
        
        private void ResetCounts()
        {
            for(int i = 0; i < myCurrencies.Length;i++)
            {
                myCurrencies[i].Count = 0;
            }
        }

        public CashRegister(Currency[] currencies)
        {
            this.myCurrencies = new Currency[currencies.Length];
            for(int i= 0;i < currencies.Length;i++)
            {
                this.myCurrencies[i] = new Currency(currencies[i].Singular, currencies[i].Plural, currencies[i].Amount,0);
            }
            Array.Sort(this.myCurrencies);
            Array.Reverse(this.myCurrencies);
        }

        public CashRegister()
        {

            this.myCurrencies = new Currency[10];
            string[,] names = {{"Fifty","Fifties" }, { "Twenty", "Twenties" }, { "Ten", "Tens" }, { "Five", "Fives" }, { "One", "Ones" },
                { "Quarter", "Quarters" }, { "Dime", "Dimes" }, { "Nickel", "Nickels" }, { "Penny", "Pennies" },  {"Benjie","Benjies" }};
            decimal[] denoms = {  50m, 20m, 10m, 5m, 1m, .25m, .1m, .05m, .01m,100m};
            for (int i = 0; i < this.myCurrencies.Length; i++)
            {
                this.myCurrencies[i] = new Currency(names[i,0],names[i,1],denoms[i], 0);

            }
            Array.Sort(this.myCurrencies);
            Array.Reverse(this.myCurrencies);
        }

        public string ProcessChange(decimal input)
        {
            ResetCounts();
            string result = "";
            for (int i = 0; i < this.myCurrencies.Length; i++)
            {
                this.myCurrencies[i].Count = (int)decimal.Floor(input / this.myCurrencies[i].Amount);
                input -= this.myCurrencies[i].Amount * this.myCurrencies[i].Count;
            }
            result += "You Received :";
            for (int i = 0; i < this.myCurrencies.Length; i++)
            {
                if (this.myCurrencies[i].Count > 0)
                {
                    if (this.myCurrencies[i].Count == 1)
                    {
                        result += " " + this.myCurrencies[i].Count + " " + this.myCurrencies[i].Singular;
                    }
                    else
                    {
                        result += " " + this.myCurrencies[i].Count + " " + this.myCurrencies[i].Plural;
                    }
                }
            }

            return result;
        }

        public string ProcessRandomChange(decimal input)
        {
            ResetCounts();
            string result = "";
            Currency curr;
            while(input > 0)
            {
                curr = GetRandomCurrencyForChange(input);
                input -= curr.Amount;
            }
            result += "\nYou Received :";
            for (int i = 0; i < this.myCurrencies.Length; i++)
            {
                if (this.myCurrencies[i].Count > 0)
                {
                    if (this.myCurrencies[i].Count == 1)
                    {
                        result += " " + this.myCurrencies[i].Count + " " + this.myCurrencies[i].Singular;
                    }
                    else
                    {
                        result += " " + this.myCurrencies[i].Count + " " + this.myCurrencies[i].Plural;
                    }
                }
            }
            return result;
        }

        public Currency GetRandomCurrencyForChange(decimal change)
        {
            
            Random rnd = new Random();
            int i = 0;
            while (change < this.myCurrencies[i].Amount && i < this.myCurrencies.Length)
            {
                i++;
            }
            int rndIDX = rnd.Next(this.myCurrencies.Length - i) + i;
            this.myCurrencies[rndIDX].Count++;
            return this.myCurrencies[rndIDX]; ;
        }
    }

    class Currency : IComparable
    {
        private string singular;
        private string plural;
        private decimal value;
        private int count;

        public int CompareTo(object obj)
        {
            Currency p = obj as Currency;
            if (this.Amount == p.Amount)
                return 0;
            else if (this.Amount < p.Amount)
                return -1;
            else
                return 1; // this.Length > p.Length
        }
        public Currency()
        {
            this.singular = "";
            this.plural = "";
            this.value = 0m;
            this.count = -1;
        }

        public Currency(string singular, string plural, decimal value, int count)
        {
            this.singular = singular;
            this.plural = plural;
            this.value = value;
            this.count = count;
        }

        public string Singular
        {
            get { return singular; }
            set { singular = value; }
        }

        public string Plural
        {
            get { return plural; }
            set { plural = value; }
        }

        public decimal Amount
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }
        public override bool Equals(object obj)
        {
            return ((obj != null) && (obj is Currency) && (this.Amount == ((Currency)obj).Amount));
        }

        public override int GetHashCode()
        {
            return this.Amount.GetHashCode();
        }
    }
}
