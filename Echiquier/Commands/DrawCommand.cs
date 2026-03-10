namespace Echiquier.Commands;

public class DrawCommand : Command
{
    public override bool CanExecuteDuringDrawProposal => true;

    public override bool Handle(string input)
        => input.Equals("/draw", StringComparison.OrdinalIgnoreCase);

    public override CommandResult Execute(Game game, string input)
    {
        if (game.IsDrawProposed)
        {
            game.Status = GameStatus.Draw;
            return CommandResult.Success(CommandStatus.Draw);
        }

        game.IsDrawProposed = true;
        return CommandResult.Success(CommandStatus.DrawProposed);
    }
}
