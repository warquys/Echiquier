namespace Echiquier.Pieces;

public abstract class Piece
{
    public const char RookSymbol    = '\u2656';
    public const char KnightSymbol  = '\u2658';
    public const char BishopSymbol  = '\u2657';
    public const char QueenSymbol   = '\u2655';
    public const char KingSymbol    = '\u2654';
    public const char PawnSymbol    = '\u2659';

    public char Symbol { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public bool HasMoved { get; set; }

    public virtual bool CanPromote => false;

    public Piece(string name, string color, char symbol)
    {
        Name = name;
        Color = color;
        Symbol = symbol;
    }

    public abstract bool IsLegalMove(Square origin, Square destination, IBoardState board);

    protected bool IsAlly(Piece? other) => other is not null && other.Color == Color;

    protected static bool IsPathClear(Square origin, Square destination, IBoardState board)
    {
        int rowDir = Math.Sign(destination.Row - origin.Row);
        int colDir = Math.Sign(destination.Column - origin.Column);

        int currentRow = origin.Row + rowDir;
        int currentCol = origin.Column + colDir;

        while (currentRow != destination.Row || currentCol != destination.Column)
        {
            if (board.GetPieceAt(new Square(currentRow, currentCol)) is not null)
                return false;
            currentRow += rowDir;
            currentCol += colDir;
        }

        return true;
    }

    public override string ToString()
    {
        return $"{Color} {Name} ({Symbol})";
    }

}
