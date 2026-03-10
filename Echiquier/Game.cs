using Echiquier.Commands;
using Echiquier.Pieces;

namespace Echiquier;

public enum GameStatus
{
    InProgress,
    Quit,
    Resigned,
    Draw
}

public class Game : IBoardState, ICommandHandler
{
    private readonly Piece[][] _board;
    private readonly List<Command> _commands = new();
    private readonly List<string> _history = new();
    private readonly List<string> _players = new();
    private int _currentPlayerIndex;

    public int Rows => _board.Length;
    public int Columns => _board[0].Length;
    public GameStatus Status { get; set; } = GameStatus.InProgress;
    public bool IsDrawProposed { get; set; }
    public IReadOnlyList<string> History => _history;
    public string CurrentPlayer => _players.Count > 0 ? _players[_currentPlayerIndex] : "";
    public Square? LastMoveFrom { get; private set; }
    public Square? LastMoveTo { get; private set; }

    /// <param name="row">En base 0.</param>
    /// <param name="colonne">En base 0.</param>
    public Piece this[int row, int colonne]
    {
        get => _board[row][colonne];
        set => _board[row][colonne] = value;
    }

    public Piece this[Square square]
    {
        get => _board[square.Row - 1][square.Column - 1];
        set => _board[square.Row - 1][square.Column - 1] = value;
    }

    public Piece? GetPieceAt(Square square) => this[square];

    public Game()
    {
        _board = new Piece[8][];
        for (int i = 0; i < 8; i++)
        {
            _board[i] = new Piece[8];
        }
    }

    public void AddPlayer(string color) => _players.Add(color);

    public void NextTurn()
    {
        if (_players.Count > 0)
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
    }

    public void SetupDefaultCommand()
    {
        Register(new MoveCommand());
        Register(new CastleCommand());
        Register(new QuitCommand());
        Register(new ResignCommand());
        Register(new DrawCommand());
        Register(new DeclineDrawCommand());
    }

    public void SetupWhite()
    {
        Piece[] row1 = _board[1];
        for (int i = 0; i < 8; i++)
        {
            row1[i] = new Pawn("White");
        }
        Piece[] row0 = _board[0];
        row0[0] = new Rook("White");
        row0[1] = new Knight("White");
        row0[2] = new Bishop("White");
        row0[3] = new Queen("White");
        row0[4] = new King("White");
        row0[5] = new Bishop("White");
        row0[6] = new Knight("White");
        row0[7] = new Rook("White");
    }

    public void SetupBlack()
    {
        Piece[] row7 = _board[6];
        for (int i = 0; i < 8; i++)
        {
            row7[i] = new Pawn("Black");
        }
        Piece[] row8 = _board[7];
        row8[0] = new Rook("Black");
        row8[1] = new Knight("Black");
        row8[2] = new Bishop("Black");
        row8[3] = new Queen("Black");
        row8[4] = new King("Black");
        row8[5] = new Bishop("Black");
        row8[6] = new Knight("Black");
        row8[7] = new Rook("Black");
    }

    public void MovePiece(Square from, Square to)
    {
        var piece = this[from];
        piece.HasMoved = true;
        LastMoveFrom = from;
        LastMoveTo = to;
        this[to] = piece;
        this[from] = null!;
    }

    public void RemovePiece(Square square) => this[square] = null!;

    public Square? FindKing(string color)
    {
        for (int r = 1; r <= Rows; r++)
            for (int c = 1; c <= Columns; c++)
            {
                var sq = new Square(r, c);
                if (GetPieceAt(sq) is King k && k.Color == color)
                    return sq;
            }
        return null;
    }

    public void Register(Command command) => _commands.Add(command);

    public CommandResult HandleInput(string input)
    {
        input = input.Trim();
        if (string.IsNullOrEmpty(input))
            return CommandResult.Failure("Commande invalide. Tapez /help pour l'aide.");

        foreach (var command in _commands)
        {
            if (command.Handle(input))
            {
                if (IsDrawProposed && !command.CanExecuteDuringDrawProposal)
                    return CommandResult.Failure("Une proposition de nulle doit d'abord être répondue.");

                var result = command.Execute(this, input);
                if (result.IsSuccess)
                    _history.Add(input);
                return result;
            }
        }
        return CommandResult.Failure("Commande invalide. Tapez /help pour l'aide.");
    }
}