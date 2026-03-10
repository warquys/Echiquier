namespace Echiquier.Commands;

public class DeclineDrawCommand : Command
{
    public override bool CanExecuteDuringDrawProposal => true;

    public override bool Handle(string input)
        => input.Equals("/decline", StringComparison.OrdinalIgnoreCase);

    public override CommandResult Execute(Game game, string input)
    {
        if (!game.IsDrawProposed)
            return CommandResult.Failure("Aucune proposition de nulle en cours.");

        game.IsDrawProposed = false;
        return CommandResult.Success(CommandStatus.DrawDeclined);
    }
}
