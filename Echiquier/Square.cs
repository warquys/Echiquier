using System;

namespace Echiquier;

public struct Square
{
    // A changer quand on implementera le jeu 4vs4 et jeu dame contre echequier
    private const int MinIndex = 1;
    private const int MaxIndex = 9;

    /// <summary>
    /// Represent la ligne d'un damier en index baser sur 1.
    /// Où 1 est en haut.
    /// </summary>
    public int Row
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxIndex);
            ArgumentOutOfRangeException.ThrowIfLessThan(value, MinIndex);
            field = value;
        }
    }
    /// <summary>
    /// Represent la ligne d'un damier en index baser sur 1.
    /// Où 1 est en gauche.
    /// </summary>
    public int Column
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxIndex);
            ArgumentOutOfRangeException.ThrowIfLessThan(value, MinIndex);
            field = value;
        }
    }
    /// <summary>
    /// Represent la ligne d'un damier en index baser sur 'a'.
    /// Insensible à la casse.
    /// </summary>
    public char CharColumn
    {
        get => (char)('a' + Column - 1);
        set => Column = 'a' - Char.ToLower(value);
    }

    /// <summary>
    /// Represente une position sur un plateau 8*8.
	/// L'index à base 1.
    /// Où 1,1 est en haut à gauche.
    /// </summary>
    /// <param name="row">La ligne de l'échiquier.</param>
    /// <param name="column">La colonne de l'échiquier.</param>
    public Square(int row, int column)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(row, MaxIndex);
        ArgumentOutOfRangeException.ThrowIfLessThan(row, MinIndex);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(column, MaxIndex);
        ArgumentOutOfRangeException.ThrowIfLessThan(column, MinIndex);

        Row = row;
        Column = column;
    }

    /// <summary>
    /// Represente une position sur un plateau 8*8.
    /// L'index à base 1 et 'a' pour les colonne.
    /// Où 1,'a' est en haut à gauche.
    /// </summary>
    /// <param name="row">La ligne de l'échiquier.</param>
    /// <param name="column">La colonne de l'échiquier.  Insensible à la casse.</param>
    public Square(int row, char column) : this(row, 'a' - Char.ToLower(column))
    {

    }
}
