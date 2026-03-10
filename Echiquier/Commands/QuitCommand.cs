namespace Echiquier.Commands;

public class QuitCommand : Command
{
    public override bool CanExecuteDuringDrawProposal => true;

    public override bool Handle(string input)
        => input.Equals("/quit", StringComparison.OrdinalIgnoreCase);

    public override CommandResult Execute(Game game, string input)
    {
        game.Status = GameStatus.Quit;
        return CommandResult.Success(CommandStatus.Quit);
    }
}
