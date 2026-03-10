using System;
using System.Collections.Generic;
using System.Text;

namespace Echiquier.CLI;

public class ConsoleDisplayer
{
    // Pas blanc et noir car sinon ça crée du blanc sur blanc et noir sur noir
    private const ConsoleColor DarkCheckerboard = ConsoleColor.DarkGray;
    private const ConsoleColor LightCheckerboard = ConsoleColor.DarkYellow;

    private readonly IBoardState _board;
    private DisplayMakers displayMakers = DisplayMakers.Top | DisplayMakers.Left;

    private readonly Dictionary<string, ConsoleColor> ColorTable = new()
    {
        { "White",  ConsoleColor.White },
        { "Black",  ConsoleColor.Black },
        { "Red",    ConsoleColor.Red },
        { "Blue",   ConsoleColor.Blue },
        { "Green",  ConsoleColor.Green },
        { "Yellow", ConsoleColor.Yellow },
    };

    public ConsoleDisplayer(Game game)
    {
        _board = game;
    }

    public void Display()
    {
        if (displayMakers.HasFlag(DisplayMakers.Top))
        {
            if (displayMakers.HasFlag(DisplayMakers.Left)) 
                Console.Write("   ");

            for (int col = 1; col <= _board.Columns; col++)
            {
                Console.Write(new Square(1, col).CharColumn + " ");
            }
            Console.WriteLine();
        }
       

        for (int row = 1; row <= _board.Rows; row++)
        {
            if (displayMakers.HasFlag(DisplayMakers.Left))
                Console.Write(row.ToString().PadLeft(2) + " ");

            for (int col = 1; col <= _board.Columns; col++)
            {
                var square = new Square(row, col);
                var piece = _board.GetPieceAt(square);
                var isDark = (row + col) % 2 == 0;
                Console.BackgroundColor = isDark ? DarkCheckerboard : LightCheckerboard;
                if (piece != null)
                {
                    if (ColorTable.TryGetValue(piece.Color, out var color))
                        Console.ForegroundColor = color;

                    Console.Write(piece.Symbol + " ");
                }
                else
                {
                    Console.Write(". ");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        if (displayMakers.HasFlag(DisplayMakers.Bottome))
        {
            if (displayMakers.HasFlag(DisplayMakers.Left))
                Console.Write("   ");

            for (int col = 1; col <= _board.Columns; col++)
            {
                Console.Write(new Square(1, col).CharColumn + " ");
            }
            Console.WriteLine();
        }
    }

    [Flags] 
    enum DisplayMakers
    {
        Top     = 1,
        Bottome = 2,
        Left    = 4,
    }

}
