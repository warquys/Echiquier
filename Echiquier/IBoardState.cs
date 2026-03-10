using Echiquier.Pieces;

namespace Echiquier;

public interface IBoardState
{
    int Rows { get; }
    int Columns { get; }
    Piece? GetPieceAt(Square square);
    Square? LastMoveFrom { get; }
    Square? LastMoveTo { get; }
}
