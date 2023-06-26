using System;
using System.Collections.Generic;

public abstract class Player : IComparable<Player>
{
    //This class creates a generic player that has a team symbol attribute and a team colour attribute.
    //The class is abstract and therefore must be overridden by its child classes: NamedPlayer and UnnamedPlayer.
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

    //This method is used to compare the NamedPlayer and UnnamedPlayer "names" to each other.

    public int CompareTo(Player other)
    {
        return string.Compare(GetPlayerName(), other.GetPlayerName());
    }

    //This method is overriden by the GetPlayerName methods from the NamedPlayer and UnnamedPlayer classes.

    public abstract string GetPlayerName();
}

public class NamedPlayer : Player
{
    //This class creates a NamedPlayer object, and is a child of the Player superclass.
    //Along with overriding the symbol and colour variables from the Player class, a NamedPlayer object also contains a name attribute.
    public string Name { get; }

    public NamedPlayer(string name, char symbol, string teamColor)
        : base(symbol, teamColor)
    {
        Name = name;
    }

    //This method overrides the GetPlayerName method related to the IComparable interface in the Player class.

    public override string GetPlayerName()
    {
        return Name;
    }
}

public class UnnamedPlayer : Player
{
    //This class creates a UnnamedPlayer object, and is a child of the Player superclass.
    //Along with overriding the symbol and colour variables from the Player class, an UnnamedPlayer object also contains a number attribute.
    public int Number { get; }

    public UnnamedPlayer(int number, char symbol, string teamColor)
        : base(symbol, teamColor)
    {
        Number = number;
    }

    //This method overrides the GetPlayerName method related to the IComparable interface in the Player class.

    public override string GetPlayerName()
    {
        string name = Number.ToString();
        return name;
    }
}

public class Board
{
    //This class creates the structure of the gameboard and determines how players execute moves.
    //It is also responsible for checking for the following: a win, a full column and a full board.

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

    //This method correctly formats the rows and columns of any given board.

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

    //This method checks if there are any available spaces left in the chossen column.

    public bool ColumnFull(int column)
    {
        return board[0, column] != ' ';
    }

    //This method correctly places the player's disk on the board.
    //It then checks for a possible win, and if no win has occured it switches the currentPlayer for the next turn.

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

    //This method accesses the other check for win methods to see if any of the 3 possible wins have occurred.

    public bool CheckForWin()
    {
        return HorizontalWin() || VerticalWin() || DiagonalWin();
    }

    //This method check the disks on the board for a horizontal win.

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

    //This method check the disks on the board for a vertical win.

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

    //This method check the disks on the board for a diagonal win.

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

    //This method switches between the 2 players.

    private Player GetNextPlayer(ref List<Player> players)
    {
        int currentPlayerIndex = players.IndexOf(currentPlayer);
        int nextPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        return players[nextPlayerIndex];
    }

    //This method creates the scructure of the board itself.

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
        Console.WriteLine("\nPick a column from 1-7.");
    }

    //This method ensures that the correct player is selected for turns and record keeping. 

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

    //This method check whether the board has any available spaces left.

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
    //This class is where the actual game is player. It begins by creating a new game, where the user has to ability to choose players.
    //The players are then prompted to make moves, within certain perameters. If these perameters are violated, this class communicated that.

    private Board board;
    private List<Player> players;

    //This method is accessed directly from the Main method and contains all necessary content, including method calls, for the game to take place.

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

    //This method creates a new game by first creating players then a clear board where gameplay takes place.

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

    //These 2 methods are responsible for automatically assigning players with the appropriate symbol and colour.

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

    //This method ensures the user enters a maximum of 2 possible players.

    private int ReadPlayerNumber()
    {
        int numPlayers;
        while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 0 || numPlayers > 2)
        {
            Console.WriteLine("Invalid input. Please enter a valid number of players (0-2):");
        }
        return numPlayers;
    }

    //This method ensures the user is only able to choose a column between 1 and 7.

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

public class Game
{
    //This class contains the main method which is only responsoble for creating a new Connect4Game object and initiating game play.
    //The user is immediately sent to the Commect4Game class to play the game.
    public static void Main(string[] args)
    {
        Connect4Game game = new Connect4Game();
        game.StartGame();
    }
}

