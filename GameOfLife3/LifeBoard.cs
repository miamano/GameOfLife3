using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife3
{
    class LifeBoard
    {
        public enum State { Living, Dying, Reborn, Dead }

        private const double RANDOM_BOARD_PARAMETER = 0.4; 

        private State[,] board;
        public int Row { set; get; }
        public int Column { set; get; }

        public LifeBoard(int row, int col)
        {
            Row = row;
            Column = col;
            board = new State[row, col];
            InitBoard();
        }

        public void CreateBoard()
        {
            int amountLifeFields = Convert.ToInt32(Row * Column * RANDOM_BOARD_PARAMETER); 
            Random randomGenerator = new Random();
            for (int i = 0; i < amountLifeFields; i++)
            {
                int tmpRow = randomGenerator.Next(Row);
                int tmpColumn = randomGenerator.Next(Column);
                
                if (board[tmpRow, tmpColumn] == State.Living)
                    amountLifeFields++;
                else
                    board[tmpRow, tmpColumn] = State.Living;
            }
        }

        public void SetCellState(int row, int col, State value)
        {
            board[row, col] = value;
        }

        public State GetCellState(int row, int col) 
        {
            return board[row, col];
        }

        public int GetNbrOfLivingCellOfBoard() 
        {
            int nbrLives = 0;
            for (int row = 0; row < Row; row++)
            {
                for (int col = 0; col < Column; col++)
                {
                    if ((board[row, col] == State.Living) || (board[row, col] == State.Reborn))
                        nbrLives++;
                }
            }
            return nbrLives;
        }

        public bool IsBoardInLife()
        {
            for (int row = 0; row < Row; row++)
            {
                for (int col = 0; col < Column; col++)
                {
                    if ((board[row, col] == State.Living) || (board[row, col] == State.Reborn))
                        return true;
                }
            }
            return false;
        }

        public int CountCellsNeighbours(int row, int col)
        {
            int startX = Math.Max(col - 1, 0);
            int endX = Math.Min(col + 1, Column - 1);
            int startY = Math.Max(row - 1, 0);
            int endY = Math.Min(row + 1, Row - 1);
            int count = 0;
            for (int r = startY; r <= endY; r++)
            {
                for (int c = startX; c <= endX; c++)
                {
                    if (c == col && r == row)
                        continue;
                    if (board[r, c] == State.Living || board[r, c] == State.Reborn)
                        count++;
                }
            }

            return count;
        }

        private void InitBoard()
        {
            for (int row = 0; row < Row; row++)
                for (int col = 0; col < Column; col++)
                    board[row, col] = State.Dead;
        }
    }
}
