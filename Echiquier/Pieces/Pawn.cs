namespace Echiquier.Pieces;

public class Pawn : Piece
{
    public override bool CanPromote => true;
    
    public Pawn(string color) : base("Pawn", color, PawnSymbol)
    {
    }


    public override bool IsLegalMove(Square origin, Square destination, IBoardState board)
    {
        int direction = Color == "White" ? 1 : -1;
        int startRow = Color == "White" ? 2 : board.Rows - 1;

        int rowDiff = destination.Row - origin.Row;
        int colDiff = Math.Abs(destination.Column - origin.Column);

        Piece? target = board.GetPieceAt(destination);

        if (colDiff == 0 && rowDiff == direction && target is null)
            return true;

        if (colDiff == 0 && rowDiff == 2 * direction && origin.Row == startRow && target is null)
        {
            Square intermediate = new(origin.Row + direction, origin.Column);
            return board.GetPieceAt(intermediate) is null;
        }

        if (colDiff == 1 && rowDiff == direction && target is not null && !IsAlly(target))
            return true;

        // passent
        if (colDiff == 1 && rowDiff == direction && target is null)
        {
            var adjacentSquare = new Square(origin.Row, destination.Column);
            var adjacentPiece = board.GetPieceAt(adjacentSquare);

            if (adjacentPiece is Pawn && !IsAlly(adjacentPiece)
                && board.LastMoveTo is Square lastTo
                && lastTo.Row == adjacentSquare.Row && lastTo.Column == adjacentSquare.Column
                && board.LastMoveFrom is Square lastFrom
                && Math.Abs(lastFrom.Row - lastTo.Row) == 2)
            {
                return true;
            }
        }

        return false;
    }
}
