﻿using Simulator.Maps;

namespace Simulator;

/// <summary>
/// Class representing a simulation of creatures moving on a map.
/// </summary>
public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<Creature> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. Bad moves are ignored.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished { get; private set; } = false;

    /// <summary>
    /// Index of the current move.
    /// </summary>
    private int _currentMoveIndex = 0;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature CurrentCreature => Creatures[_currentMoveIndex % Creatures.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName => Moves[_currentMoveIndex % Moves.Length].ToString().ToLower();

    /// <summary>
    /// Simulation constructor.
    /// </summary>
    /// <param name="map">Simulation map.</param>
    /// <param name="creatures">List of creatures.</param>
    /// <param name="positions">List of starting positions.</param>
    /// <param name="moves">Cyclic list of moves.</param>
    /// <exception cref="ArgumentException">Thrown if the list of creatures is empty or lists have different lengths.</exception>
    public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
    {
        if (creatures == null || creatures.Count == 0)
        {
            throw new ArgumentException("The list of creatures cannot be empty.");
        }

        if (creatures.Count != positions.Count)
        {
            throw new ArgumentException("The number of creatures must match the number of positions.");
        }

        Map = map ?? throw new ArgumentNullException(nameof(map));
        Creatures = creatures;
        Positions = new List<Point>(positions);
        Moves = string.Concat(moves.Where(c => "urdlURDL".Contains(c))); // Filter valid moves
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if simulation is finished.</exception>
    public void Turn()
    {
        if (Finished)
        {
            throw new InvalidOperationException("The simulation is already finished.");
        }

        // Determine current move
        var currentMoveChar = Moves[_currentMoveIndex % Moves.Length];
        var currentDirection = DirectionParser.Parse(currentMoveChar.ToString()).FirstOrDefault();

        // Get current creature and its position
        var creatureIndex = _currentMoveIndex % Creatures.Count;
        var currentPosition = Positions[creatureIndex];

        // Compute the next position
        var nextPosition = Map.Next(currentPosition, currentDirection);

        // Update position if valid
        if (Map.Exist(nextPosition))
        {
            Positions[creatureIndex] = nextPosition;
        }

        // Increment move index
        _currentMoveIndex++;

        // Check if all moves have been processed
        if (_currentMoveIndex >= Moves.Length * Creatures.Count)
        {
            Finished = true;
        }
    }
}