using System.Text.RegularExpressions;
using Echiquier.Pieces;

namespace Echiquier.Commands;

public class CastleCommand : Command
{
    private static readonly Regex CastlePattern = new("^(O-O-O|O-O|0-0-0|0-0)$", RegexOptions.IgnoreCase);

    public override bool Handle(string input)
        => CastlePattern.IsMatch(input);

    public override CommandResult Execute(Game game, string input)
    {
        var kingSquare = game.FindKing(game.CurrentPlayer);
        if (kingSquare is not Square ks)
            return CommandResult.Failure("Roi introuvable.");

        var king = game.GetPieceAt(ks)!;
        if (king.HasMoved)
            return CommandResult.Failure("Le roi a déjà bougé.");

        var isQueenside = input.Length > 3;

        int rookColumn = isQueenside ? 1 : game.Columns;
        var rookSquare = new Square(ks.Row, rookColumn);
        var rook = game.GetPieceAt(rookSquare);

        if (rook is not Rook || rook.Color != game.CurrentPlayer)
            return CommandResult.Failure("Aucune tour valide pour le roque.");

        if (rook.HasMoved)
            return CommandResult.Failure("La tour a déjà bougé.");

        int direction = isQueenside ? -1 : 1;
        int col = ks.Column + direction;
        while (col != rookColumn)
        {
            if (game.GetPieceAt(new Square(ks.Row, col)) is not null)
                return CommandResult.Failure("Le chemin n'est pas libre pour le roque.");
            col += direction;
        }

        var newKingSquare = new Square(ks.Row, ks.Column + 2 * direction);
        var newRookSquare = new Square(ks.Row, ks.Column + direction);

        game.MovePiece(ks, newKingSquare);
        game.MovePiece(rookSquare, newRookSquare);
        game.NextTurn();

        string side = isQueenside ? "grand" : "petit";
        return CommandResult.Success(CommandStatus.Continue, $"roque {side} ({king.Symbol})");
    }
}
