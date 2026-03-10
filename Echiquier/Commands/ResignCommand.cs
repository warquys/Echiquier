namespace Echiquier.Commands;

public class ResignCommand : Command
{
    public override bool Handle(string input)
        => input.Equals("/resign", StringComparison.OrdinalIgnoreCase);

    public override CommandResult Execute(Game game, string input)
    {
        game.Status = GameStatus.Resigned;
        return CommandResult.Success(CommandStatus.Resign);
    }
}
