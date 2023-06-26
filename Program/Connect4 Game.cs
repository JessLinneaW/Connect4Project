using System;
using System.Collections.Generic;

public abstract class Player : IComparable<Player>
{
    public char Symbol { get; }
    public string TeamColor { get; }

    protected Player(char symbol, string teamColor)
    {
        Symbol = symbol;
        TeamColor = teamColor;
    }

    protected Player(char symbol) : this(symbol, string.Empty)
    {
    }

    public int CompareTo(Player other)
    {
        return string.Compare(GetPlayerName(), other.GetPlayerName());
    }

    public abstract string GetPlayerName();
}

public class NamedPlayer : Player
{
    public string Name { get; }

    public NamedPlayer(string name, char symbol, string teamColor)
        : base(symbol, teamColor)
    {
        Name = name;
    }

    public override string GetPlayerName()
    {
        return Name;
    }
}

public class UnnamedPlayer : Player
{
    public int Number { get; }

    public UnnamedPlayer(int number, char symbol, string teamColor)
        : base(symbol, teamColor)
    {
        Number = number;
    }

    public override string GetPlayerName()
    {
        string name = Number.ToString();
        return name;
    }
}

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

public class Connect4Game
{
    private Board board;
    private List<Player> players;

    public void StartGame()
    {
        bool playAgain = true;
        do
        {
            InitializeGame();
            while (true)
            {
                board.PrintBoard();

                int column = ReadColumnChoice();
                while (board.ColumnFull(column - 1))
                {
                    Console.WriteLine("That column is full. Choose another column.");
                    column = ReadColumnChoice();
                }

                board.Move(column, ref players);

                if (board.CheckForWin())
                {
                    board.PrintBoard();
                    Console.WriteLine($"Congratulations! {board.GetCurrentPlayerName()} wins!");
                    break;
                }

                if (board.IsBoardFull())
                {
                    board.PrintBoard();
                    Console.WriteLine("It's a draw! The board is full.");
                    break;
                }
            }
            Console.WriteLine("Game over. Would you like to play again? (Y/N)");
            string playAgainInput = Console.ReadLine();
            playAgain = playAgainInput.Equals("Y", StringComparison.OrdinalIgnoreCase) || playAgainInput.Equals("y", StringComparison.OrdinalIgnoreCase);
            if (playAgain) Console.Clear();
        } while (playAgain);
    }

    private void InitializeGame()
    {
        players = new List<Player>();
        Console.WriteLine("Enter the number of named players (or enter 0 for unnamed players only):");
        int numNamedPlayers = ReadPlayerNumber();

        for (int i = 1; i <= numNamedPlayers; i++)
        {
            Console.WriteLine($"Enter the name of Player {i}:");
            string name = Console.ReadLine();

            players.Add(new NamedPlayer(name, GetSymbolForPlayer(i), GetColorForPlayer(i)));
        }

        int numUnnamedPlayers = 2 - numNamedPlayers;
        for (int i = 1; i <= numUnnamedPlayers; i++)
        {
            players.Add(new UnnamedPlayer(numNamedPlayers + i, GetSymbolForPlayer(numNamedPlayers + i), GetColorForPlayer(numNamedPlayers + i)));
        }

        board = new Board(players);
    }

    private char GetSymbolForPlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return 'X';
        }
        else
        {
            return 'O';
        }
    }

    private string GetColorForPlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return "blue";
        }
        else
        {
            return "red";
        }
    }



    private int ReadPlayerNumber()
    {
        int numPlayers;
        while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number of players:");
        }
        return numPlayers;
    }


    private int ReadColumnChoice()
    {
        int column;
        while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
        {
            Console.WriteLine("Invalid input. Please enter a valid column number (1-7):");
        }
        return column;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Connect4Game game = new Connect4Game();
        game.StartGame();
    }
}
