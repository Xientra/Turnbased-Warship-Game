using UnityEngine;

public class Tile
{
	public readonly Vector2Int coordinates;

	public Vector3 Position { get => CoordinatesToPosition(coordinates); }

	/// <summary> Creates a tile with coordinates nearest to the given point. </summary>
	public Tile(Vector3 position)
	{
		coordinates = PositionToCoordinates(position);
	}
	public Tile(int x, int y)
	{
		coordinates = new Vector2Int(x, y);
	}
	public Tile(Vector2Int coordinates)
	{
		this.coordinates = coordinates;
	}


	// calculate distace to another tile
	public int Distance(Vector3 position)
	{
		return Distance(PositionToCoordinates(position));
	}
	public int Distance(Tile tile)
	{
		return Distance(tile.coordinates);
	}
	public int Distance(Vector2Int coordinates)
	{
		return Mathf.Abs(this.coordinates.x - coordinates.x) + Mathf.Abs(this.coordinates.y - coordinates.y);
	}

	// ---------- static methods ---------- //

	/// <summary> Returns the center position to the given coordinates. </summary>
	public static Vector3 CoordinatesToPosition(Vector2Int coordinates)
	{
		return new Vector3(coordinates.x + 0.5f, coordinates.y + 0.5f, 0);
	}

	/// <summary> Returns the coordinates that respond to the given position. </summary>
	public static Vector2Int PositionToCoordinates(Vector3 position)
	{
		return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
	}

	/// <summary> Returns the center position of the tile, the given position is in. </summary>
	public static Vector3 SnapToGrid(Vector3 position)
	{
		return CoordinatesToPosition(PositionToCoordinates(position));
	}

	public static int Distance(Tile t1, Tile t2)
	{
		return Tile.Distance(t1.coordinates, t2.coordinates);
	}
	public static int Distance(Vector2Int coords1, Vector2Int coords2)
	{
		return Mathf.Abs(coords1.x - coords2.x) + Mathf.Abs(coords1.y - coords2.y);
	}
}
