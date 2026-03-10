using Echiquier;
using Echiquier.CLI;
using Echiquier.Commands;

var game = new Game();
game.AddPlayer("White");
game.AddPlayer("Black");
game.SetupWhite();
game.SetupBlack();
game.SetupDefaultCommand();
game.Register(new HelpCommand());

var displayer = new ConsoleDisplayer(game);
displayer.Display();

while (game.Status == GameStatus.InProgress)
{
    Console.Write($"[{game.CurrentPlayer}] Coup (eg. a1a8, O-O, O-O-O) ? ");
    var input = Console.ReadLine();

    if (input is null)
        break;

    var result = game.HandleInput(input);

    if (!result.IsSuccess)
    {
        Console.WriteLine($"Erreur : {result.Message}");
        continue;
    }

    switch (result.Status)
    {
        case CommandStatus.Continue when result.Message is not null:
            Console.WriteLine($"-> {result.Message}");
            goto case CommandStatus.Continue;
        case CommandStatus.Continue:
            displayer.Display();
            break;
        case CommandStatus.Quit:
            Console.WriteLine("Partie terminée.");
            break;
        case CommandStatus.Resign:
            Console.WriteLine("Abandon !");
            break;
        case CommandStatus.Draw:
            Console.WriteLine("Partie nulle !");
            break;
        case CommandStatus.DrawProposed:
            Console.WriteLine("Proposition de nulle. L'adversaire doit accepter (/draw) ou refuser (/decline).");
            break;
        case CommandStatus.DrawDeclined:
            Console.WriteLine("Proposition de nulle refusée.");
            break;
    }
}

