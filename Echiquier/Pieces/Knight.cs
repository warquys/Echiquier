namespace Echiquier.Pieces;

public class Knight : Piece
{
    public Knight(string color) : base("Knight", color, KnightSymbol)
    {
    }

    public override bool IsLegalMove(Square origin, Square destination, IBoardState board)
    {
        int rowDiff = Math.Abs(destination.Row - origin.Row);
        int colDiff = Math.Abs(destination.Column - origin.Column);

        if ((rowDiff != 2 || colDiff != 1) && (rowDiff != 1 || colDiff != 2))
            return false;

        return !IsAlly(board.GetPieceAt(destination));
    }
}
