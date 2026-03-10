using System.Text.RegularExpressions;
using Echiquier.Pieces;

namespace Echiquier.Commands;

public class MoveCommand : Command
{
    private static readonly Regex MovePattern = new("^[a-h][1-8][a-h][1-8]$");

    public Piece? MovedPiece { get; private set; }

    public override bool Handle(string input)
    {
        if (!MovePattern.IsMatch(input))
            return false;

        return true;
    }

    public override CommandResult Execute(Game game, string input)
    {
        var from = new Square(input[1] - '0', input[0] - 'a' + 1);
        var to   = new Square(input[3] - '0', input[2] - 'a' + 1);

        var piece = game.GetPieceAt(from);
        if (piece is null)
            return CommandResult.Failure("Aucune pièce sur la case de départ.");

        if (piece.Color != game.CurrentPlayer)
            return CommandResult.Failure($"Ce n'est pas le tour de {piece.Color}.");

        if (!piece.IsLegalMove(from, to, game))
            return CommandResult.Failure($"Coup illégal pour {piece}.");

        // Prise en passant : pion se déplace en diagonale vers une case vide
        string? captureInfo = null;
        if (piece is Pawn && from.Column != to.Column && game.GetPieceAt(to) is null)
        {
            var capturedSquare = new Square(from.Row, to.Column);
            game.RemovePiece(capturedSquare);
            captureInfo = " en passant";
        }

        game.MovePiece(from, to);
        game.NextTurn();
        MovedPiece = piece;
        var moveStr = $"{from.CharColumn}{from.Row}{to.CharColumn}{to.Row}";
        return CommandResult.Success(CommandStatus.Continue, $"déplacement de {piece.Symbol}  {moveStr}{captureInfo}");
    }
}
