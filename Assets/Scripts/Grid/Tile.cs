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

	// utility methods
	private Vector3 CoordinatesToPosition(Vector2Int tile)
	{
		return new Vector3(coordinates.x + 0.5f, coordinates.y + 0.5f, 0);
	}

	public static Vector2Int PositionToCoordinates(Vector3 position)
	{
		return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
	}

	// static methods //

	public static int Distance(Tile t1, Tile t2)
	{
		return Tile.Distance(t1.coordinates, t2.coordinates);
	}
	public static int Distance(Vector2Int coords1, Vector2Int coords2)
	{
		return Mathf.Abs(coords1.x - coords2.x) + Mathf.Abs(coords1.y - coords2.y);
	}
}
