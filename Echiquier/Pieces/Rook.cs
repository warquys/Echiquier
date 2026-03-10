namespace Echiquier.Pieces;

public class Rook : Piece
{
    public Rook(string color) : base("Rook", color, RookSymbol)
    {
    }

    public override bool IsLegalMove(Square origin, Square destination, IBoardState board)
    {
        if (origin.Row != destination.Row && origin.Column != destination.Column)
            return false;

        if (!IsPathClear(origin, destination, board))
            return false;

        return !IsAlly(board.GetPieceAt(destination));
    }
}
