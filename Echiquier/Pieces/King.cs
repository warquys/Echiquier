namespace Echiquier.Pieces;

public class King : Piece
{
    public King(string color) : base("King", color, KingSymbol)
    {
    }

    public override bool IsLegalMove(Square origin, Square destination, IBoardState board)
    {
        int rowDiff = Math.Abs(destination.Row - origin.Row);
        int colDiff = Math.Abs(destination.Column - origin.Column);

        if (rowDiff > 1 || colDiff > 1 || (rowDiff == 0 && colDiff == 0))
            return false;

        return !IsAlly(board.GetPieceAt(destination));
    }
}
