using System;

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
