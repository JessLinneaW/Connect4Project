using System;
using System.Data.Common;

namespace Final_Project___Connect4
{
    // Controller.cs
    class Controller
    {
        private Board board;
        private HumanPlayer humanPlayer;
        private ComputerPlayer computerPlayer;
        private Communication comm;

        public Controller()
        {
            board = new Board();
            humanPlayer = new HumanPlayer();
            computerPlayer = new ComputerPlayer();
            comm = new Communication();
        }

        public void GamePlay()
        {
            // Game logic
        }
    }

    // Board.cs
    class Board
    {
        private const int numRows = 6;
        private const int numColumns = 7;
        private int[,] board;
        private char currentPlayer;

        public Board()
        {
            board = new int[numRows, numColumns];
            currentPlayer = 'X';
            //InitializeBoard();
        }

        public bool ColumnFull(int column)
        {
            return board[0, column] != ' ';
        }

        public bool BoardFull()
        {
            for (int column = 0; column < numColumns; column++)
            {
                if (!ColumnFull(column)) return false;
            }

            return true;
        }

        public void Move(int column)
        {
            for (int row = numRows - 1; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = currentPlayer;
                    break;
                }
            }

            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        public bool checkForWin()
        {
            return HorizontalWin() || VerticalWin() || DiagonalWin();
        }

        private bool HorizontalWin()
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column <= numColumns - 4; column++)
                {
                    if (board[row, column] != ' ' &&
                        board[row, column] == board[row, column + 1] &&
                        board[row, column] == board[row, column + 2] &&
                        board[row, column] == board[row, column + 3])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool VerticalWin()
        {
            for (int row = 0; row <= numRows - 4; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    if (board[row, column] != ' ' &&
                        board[row, column] == board[row + 1, column] &&
                        board[row, column] == board[row + 2, column] &&
                        board[row, column] == board[row + 3, column])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DiagonalWin()
        {
            for (int row = 0; row <= numRows - 4; row++)
            {
                for (int column = 0; column <= numColumns - 4; column++)
                {
                    if (board[row, column] != ' ' &&
                       (board[row, column] == board[row + 1, column + 1] &&
                       board[row, column] == board[row + 2, column + 2] &&
                       board[row, column] == board[row + 3, column + 3]))
                    {
                        return true;
                    }

                    if (column >= 3 && board[row, column + 3] != ' ' &&
                        board[row, column + 3] == board[row + 1, column + 2] &&
                        board[row, column + 3] == board[row + 2, column + 1] &&
                        board[row, column + 3] == board[row + 3, column])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void InitializeBoard()
        {
            for (int row = 0; row <= numRows; row++)
            {
                for (int column = 0; column <= numColumns; column++)
                {
                    board[row, column] = ' ';
                }
            }
        }

        public char GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public int DiscPosition(int row, int column)
        {
            return board[row, column];
        }

        public void PrintBoard()
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    Console.WriteLine("|" + board[row, column]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("----------");
        }

        /*
        -- is column full?
        -- is board full?
        -- check for win
           -- horizontal
           -- vertical
           -- diagonal
        -- make move
         */

        //private int FreeTiles free;
        //private int CurrentPlayer current;
        // Board implementation
    }

    // Player.cs
    public abstract class Player
    {

        private string Colour;
        private char Team;
        //private int[,] Position;
        // Common player properties and methods go here
    }

    // HumanPlayer.cs
    class HumanPlayer : Player
    {
        // Human player implementation goes here
        private string Name;
    }

    // ComputerPlayer.cs
    class ComputerPlayer : Player
    {
        // Computer player implementation goes here
        private int Number;
    }

    // Communication.cs
    class Communication
    {
        // Console communication methods go here
    }

}
