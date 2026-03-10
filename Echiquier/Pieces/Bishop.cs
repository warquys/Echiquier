namespace Echiquier.Pieces;

public class Bishop : Piece
{
    public Bishop(string color) : base("Bishop", color, BishopSymbol)
    {
    }

    public override bool IsLegalMove(Square origin, Square destination, IBoardState board)
    {
        int rowDiff = Math.Abs(destination.Row - origin.Row);
        int colDiff = Math.Abs(destination.Column - origin.Column);

        if (rowDiff != colDiff || rowDiff == 0)
            return false;

        if (!IsPathClear(origin, destination, board))
            return false;

        return !IsAlly(board.GetPieceAt(destination));
    }
}
