using Echiquier.Commands;

namespace Echiquier.CLI;

public class HelpCommand : Command
{
    public override bool CanExecuteDuringDrawProposal => true;

    public override bool Handle(string input)
        => input.Equals("/help", StringComparison.OrdinalIgnoreCase);

    public override CommandResult Execute(Game game, string input)
    {
        Console.WriteLine(
            """
            === Aide ===
              a1a8        Déplacer une pièce (case départ + case arrivée)
              O-O         Petit roque (côté roi)
              O-O-O       Grand roque (côté dame)
              /quit       Quitter la partie
              /resign     Abandonner
              /draw       Proposer la nulle
              /decline    Refuser la nulle
              /help       Afficher cette aide
            =============
            """);
        return CommandResult.Success(CommandStatus.Continue);
    }
}
