using System;

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
