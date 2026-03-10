namespace Echiquier.Pieces;

public class Queen : Piece
{
    public Queen(string color) : base("Queen", color, QueenSymbol)
    {
    }

    public override bool IsLegalMove(Square origin, Square destination, IBoardState board)
    {
        int rowDiff = Math.Abs(destination.Row - origin.Row);
        int colDiff = Math.Abs(destination.Column - origin.Column);

        bool isStraight = origin.Row == destination.Row || origin.Column == destination.Column;
        bool isDiagonal = rowDiff == colDiff && rowDiff != 0;

        if (!isStraight && !isDiagonal)
            return false;

        if (!IsPathClear(origin, destination, board))
            return false;

        return !IsAlly(board.GetPieceAt(destination));
    }
}
