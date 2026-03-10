namespace Echiquier.Commands;

public readonly struct CommandResult
{
    public bool IsSuccess { get; init; }
    public CommandStatus Status { get; init; }
    public string? Message { get; init; }

    public static CommandResult Success(CommandStatus status, string? message = null)
        => new() { IsSuccess = true, Status = status, Message = message };

    public static CommandResult Failure(string message)
        => new() { IsSuccess = false, Message = message };

    public override string ToString() => Message ?? String.Empty;
}
