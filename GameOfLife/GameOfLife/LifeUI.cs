using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife3
{
    class LifeUI
    {
        private const string POPULATION_DEAD = "DEAD"; 
        private const string POPULATION_DECREASE = "DECREASE";
        private const string POPULATION_STAGNATE = "STAGNATE";
        private const string POPULATION_INCREASE = "INCREASE";
        private const string POPULATION_START = "NOTHING TO COMPARE WITH";

        private string state = string.Empty;
 
        public void DrawBoard(LifeBoard board, int currGeneration, int populationState, int livingCells)
        {
            switch (populationState)
            {
                case 0:

                    state = POPULATION_DEAD;
                    break;
                case 1:
                    state = POPULATION_DECREASE;
                    break;
                case 2:
                    state = POPULATION_STAGNATE;
                    break;
                case 3:
                    state = POPULATION_INCREASE;
                    break;
                case 4:
                    state = POPULATION_START;
                    break;

                default:
                    state = "00";
                    break;
            }

            Console.Clear();
            Console.WriteLine();
            Console.Write("Generation: {0} | Living:  {1} | State: {2}", currGeneration, livingCells, state); 
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            //Simple utskrift
            for (int row = 0; row < board.Row; row++)
            {
                for (int col = 0; col < board.Column; col++)
                {
                    string tmp = String.Empty;
                    switch (board.GetCellState(row, col))
                    {
                        case LifeBoard.State.Living:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            tmp = "x";
                            break;

                        case LifeBoard.State.Reborn:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            tmp = "+";
                            break;

                        case LifeBoard.State.Dying:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            tmp = "o";
                            break;

                        case LifeBoard.State.Dead:
                            //Console.BackgroundColor = ConsoleColor.White;
                            tmp = " ";
                            break;

                        default:
                            tmp = "-";
                            break;

                    }
                    
                    Console.Write("{0}", tmp);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public static void Welcome()
        {
            Console.Clear(); 

            Console.WriteLine();
            
            string title = "Game of LIFE";
            Console.WriteLine(title);
            DrawLine(title.Length);
            Console.WriteLine();
            Console.WriteLine("Enter the preferd size (max. {0}) of the gameboard and the number of generations that will be shown at the time.", Program.MAX_SIZE);
            Console.WriteLine();
        }

        public static void DrawLine(int length)
        {
            for (int i = 0; i < length - 1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("-");
        }
    }
}
