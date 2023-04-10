using System;
using PuzzleBoard.Domain.Interfaces;

#nullable enable

namespace PuzzleBoard.Domain.Models;

/// <summary>
/// In-memory storage class that keeps track of how many intersections a word would have
/// if it started from the defined location.  Used by the <see cref="IPlacementChooser"/>
/// to make decisions between possible word locations.
/// </summary>
public class StartingPosition
{
    private readonly int _row, _col, _intersects;
    private readonly WordDirections _direction;

    public WordDirections Direction
    {
        get { return _direction; }
    }

    public int Col
    {
        get { return _col; }
    }

    public int Row
    {
        get { return _row; }
    }

    public int Intersects
    {
        get { return _intersects; }
    }

    public StartingPosition(int row, int col, WordDirections directions, int intersects = 0)
    {
        _row = row;
        _col = col;
        _direction = directions;
        _intersects = intersects;
    }

    public StartingPosition(StartingPosition source, int intersects = 0)
    {
        _row = source._row;
        _col = source._col;
        _direction = source._direction;
        _intersects = intersects;
    }
}