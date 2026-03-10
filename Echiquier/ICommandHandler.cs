using Echiquier.Commands;

namespace Echiquier;

public interface ICommandHandler
{
    void Register(Command command);
    CommandResult HandleInput(string input);
}
