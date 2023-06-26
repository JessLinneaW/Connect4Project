using System;

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
