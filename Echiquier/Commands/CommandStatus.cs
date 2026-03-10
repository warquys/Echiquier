using System.Text.RegularExpressions;
using Echiquier.Pieces;

namespace Echiquier.Commands;

public enum CommandStatus
{
    Continue,
    Quit,
    Resign,
    DrawProposed,
    Draw,
    DrawDeclined
}
