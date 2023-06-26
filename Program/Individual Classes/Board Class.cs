using System;

public class Board
{
    private const int numRows = 6;
    private const int numColumns = 7;
    private char[,] board;
    private Player currentPlayer;

    public Board(List<Player> players)
    {
        board = new char[numRows, numColumns];
        InitializeBoard();
        currentPlayer = players[0];
    }

    private void InitializeBoard()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                board[row, column] = ' ';
            }
        }
    }

    public bool ColumnFull(int column)
    {
        return board[0, column] != ' ';
    }

    public void Move(int column, ref List<Player> players)
    {
        for (int row = numRows - 1; row >= 0; row--)
        {
            if (board[row, column - 1] == ' ')
            {
                board[row, column - 1] = currentPlayer.Symbol;
                if (CheckForWin())
                {
                    return;
                }
                currentPlayer = GetNextPlayer(ref players);
                break;
            }
        }
    }


    public bool CheckForWin()
    {
        return HorizontalWin() || VerticalWin() || DiagonalWin();
    }

    private bool HorizontalWin()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column <= numColumns - 4; column++)
            {
                if (board[row, column] == currentPlayer.Symbol &&
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
                if (board[row, column] == currentPlayer.Symbol &&
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
                if (board[row, column] == currentPlayer.Symbol &&
                    board[row, column] == board[row + 1, column + 1] &&
                    board[row, column] == board[row + 2, column + 2] &&
                    board[row, column] == board[row + 3, column + 3])
                {
                    return true;
                }
            }
        }

        for (int row = 3; row < numRows; row++)
        {
            for (int column = 0; column <= numColumns - 4; column++)
            {
                if (board[row, column] == currentPlayer.Symbol &&
                    board[row, column] == board[row - 1, column + 1] &&
                    board[row, column] == board[row - 2, column + 2] &&
                    board[row, column] == board[row - 3, column + 3])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private Player GetNextPlayer(ref List<Player> players)
    {
        int currentPlayerIndex = players.IndexOf(currentPlayer);
        int nextPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        return players[nextPlayerIndex];
    }

    public void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine("Connect 4");
        Console.WriteLine();

        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                Console.Write("| " + board[row, column] + " ");
            }
            Console.WriteLine("|");
        }

        Console.WriteLine("-----------------------------");
        Console.WriteLine("  1   2   3   4   5   6   7");
        Console.WriteLine();
        Console.WriteLine($"Current Player: {GetCurrentPlayerName()}");
    }

    public string GetCurrentPlayerName()
    {
        if (currentPlayer is NamedPlayer namedPlayer)
        {
            return namedPlayer.Name;
        }
        else if (currentPlayer is UnnamedPlayer unnamedPlayer)
        {
            return $"Player {unnamedPlayer.Number}";
        }
        else
        {
            return string.Empty;
        }
    }

    public bool IsBoardFull()
    {
        for (int column = 0; column < numColumns; column++)
        {
            if (!ColumnFull(column))
            {
                return false;
            }
        }
        return true;
    }
}
