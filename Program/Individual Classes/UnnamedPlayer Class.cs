using System;

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
