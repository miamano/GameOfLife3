using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife3
{
    class Program
    {
        public const int MAX_SIZE = 30; 

        static void Main(string[] args)
        {
            bool isContinue = true;
            while (isContinue)
            {
                LifeUI.Welcome();
                int row = InputInt("Number of rows: ");
                int column = InputInt("Number of columns: ");
                int generations = InputInt("Number of generations: ");

                new LifeGame(row, column, generations);

                bool isValidChar = false;
                while (!isValidChar)
                {
                    string text = "GAME OVER \n\nNew game (y/n)? ";
                    Console.WriteLine();
                    LifeUI.DrawLine(text.Length);
                    Console.Write(text);
                    string cont = Console.ReadLine();
                    isValidChar = (cont.ToUpper() == "N" || cont.ToUpper() == "Y") ? true : false;
                    isContinue = (isValidChar && cont.ToUpper() == "Y") ? true : false;
                }
            }
        }

        private static int InputInt(String text)
        {
            int number = 0;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write(text);
                isValid = int.TryParse(Console.ReadLine(), out number);
                isValid = (isValid && number <= MAX_SIZE && number > 0) ? true : false;

                if (!isValid)
                    Console.WriteLine("That is not a valid input. Plz try again.");
            }
            return number;
        }
    }
}
