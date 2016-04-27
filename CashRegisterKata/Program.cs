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
            Console.WriteLine(ProcessChange(input));
            Console.ReadKey();

        }


        static string ProcessChange(decimal input)
        {
            string[,] names = { {"Benjie","Benjies" },{"Fifty","Fifties" }, { "Twenty", "Twenties" }, { "Ten", "Tens" }, { "Five", "Fives" }, { "One", "Ones" },
                { "Quarter", "Quarters" }, { "Dime", "Dimes" }, { "Nickel", "Nickels" }, { "Penny", "Pennies" } };
            decimal[] denoms = { 100m, 50m, 20m, 10m, 5m, 1m, .25m, .1m, .05m, .01m };
            int[] count = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string result = "";
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = (int)decimal.Floor(input / denoms[i]);
                input -= denoms[i] * count[i];
            }
            result += "You Received :";
            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > 0)
                {
                    if (count[i] == 1)
                    {
                        result += " " + count[i] + " " + names[i, 0];
                        //Console.Write("{0} {1},", count[i], names[i, 0]);
                    }
                    else
                    {
                        result += " " + count[i] + " " + names[i, 1];
                        //Console.Write("{0} {1},", count[i], names[i, 1]);
                    }
                }
            }

            return result;
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
}
