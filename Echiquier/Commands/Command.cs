namespace Echiquier.Commands;

public abstract class Command
{
    public virtual bool CanExecuteDuringDrawProposal => false;
    public abstract bool Handle(string input);
    public abstract CommandResult Execute(Game game, string input);
}
