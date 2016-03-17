using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfLife3
{
    class LifeGame
    {
        private const int SLEEP = 100;
        private const int POPULATION_DEAD = 0;
        private const int POPULATION_DECREASE = 1;
        private const int POPULATION_STAGNATE = 2;
        private const int POPULATION_INCREASE = 3;
        private const int POPULATION_START = 4;

        private LifeBoard currentGeneration;
        private LifeUI lifeUI;
        private int maxGeneration;
        private int roundGenerationCounter; //räknar antalet generation i EN runda
        private int allGenerationCounter; //count all the generation in all round

        public LifeGame(int row, int col, int maxGeneration)
        {
            this.maxGeneration = maxGeneration;
            roundGenerationCounter = 1;
            allGenerationCounter = 1;

            currentGeneration = new LifeBoard(row, col);
            lifeUI = new LifeUI();
            currentGeneration.CreateBoard();
            lifeUI.DrawBoard(currentGeneration, allGenerationCounter, POPULATION_START, currentGeneration.GetNbrOfLivingCellOfBoard());
            StartGame();
        }

        private void StartGame()
        {
            roundGenerationCounter = 1;
            Console.Write("Press <enter> to start");
            Console.ReadLine();

            while (true)
            {
                int neighbours = 0;
                LifeBoard newGeneration = new LifeBoard(currentGeneration.Row, currentGeneration.Column);
                for (int row = 0; row < currentGeneration.Row; row++)
                {
                    for (int col = 0; col < currentGeneration.Column; col++)
                    {
                        neighbours = currentGeneration.CountCellsNeighbours(row, col);
                        newGeneration.SetCellState(row, col, Rules(neighbours, currentGeneration.GetCellState(row, col)));
                    }
                }

                roundGenerationCounter++;
                allGenerationCounter++;

                bool dead = !newGeneration.IsBoardInLife() ? true : false;
                bool stagnate = CompareGenerations(currentGeneration, newGeneration) ? true : false;

                if (dead || stagnate)
                {
                    Thread.Sleep(SLEEP);
                    currentGeneration = newGeneration;
                    lifeUI.DrawBoard(currentGeneration,
                                    allGenerationCounter,
                                    dead ? POPULATION_DEAD : POPULATION_STAGNATE,
                                    currentGeneration.GetNbrOfLivingCellOfBoard());
                    break;
                }

                int prev = 0;
                int next = 0;
                int state = 0;

                prev = currentGeneration.GetNbrOfLivingCellOfBoard();
                next = newGeneration.GetNbrOfLivingCellOfBoard();
                if (prev < next)
                    state = POPULATION_INCREASE;
                else if (prev == next)
                    state = POPULATION_STAGNATE;
                else
                    state = POPULATION_DECREASE;

                Thread.Sleep(SLEEP);
                currentGeneration = newGeneration;
                lifeUI.DrawBoard(currentGeneration, allGenerationCounter, state, currentGeneration.GetNbrOfLivingCellOfBoard());

                if (roundGenerationCounter > maxGeneration)
                {
                    roundGenerationCounter = 1;
                    Console.WriteLine("Denna runda är över.");
                    Console.Write("Press <enter> for new generation or (Q)uit.");
                    if (Console.ReadLine().ToUpper() == "Q")
                        break;
                }
            }
        }


        private bool CompareGenerations(LifeBoard current, LifeBoard next)
        {
            if (current.Row != next.Row && current.Column != next.Column) return false;

            for (int row = 0; row < current.Row; row++)
            {
                for (int col = 0; col < current.Column; col++)
                {
                    if (current.GetCellState(row, col) != next.GetCellState(row, col)) return false;
                }
            }
            return true;
        }

        private LifeBoard.State Rules(int neighbours, LifeBoard.State life)
        {
            if (life == LifeBoard.State.Dying) life = LifeBoard.State.Dead;
            if (life == LifeBoard.State.Reborn) life = LifeBoard.State.Living;

            if ((life == LifeBoard.State.Living) && neighbours < 2) return LifeBoard.State.Dying;
            if ((life == LifeBoard.State.Living) && (neighbours == 2 || neighbours == 3)) return LifeBoard.State.Living;
            if ((life == LifeBoard.State.Living) && neighbours > 3) return LifeBoard.State.Dying;
            if ((life == LifeBoard.State.Dead) && neighbours == 3) return LifeBoard.State.Reborn;

            return life;
        }
    }
}
