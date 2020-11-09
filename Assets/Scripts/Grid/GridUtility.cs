using System;
using UnityEngine;

public static class GridUtility
{

	/// <summary> Returns the center position of the given tile. </summary>
	[Obsolete("Use Tile.Position instead.")]
	public static Vector3 TileToPosition(Vector2Int tile)
	{
		return new Vector3(tile.x + 0.5f, tile.y + 0.5f, 0);
	}

	/// <summary> Returns the tile the given position lies in. </summary>
	[Obsolete("Use Tile.PositionToCoordinates(Vector3 position) instead.")]
	public static Vector2Int PositionToTile(Vector3 position)
	{
		return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
	}

	/// <summary> Returns the center position of the tile, the given position is in. </summary>
	public static Vector3 SnapToGrid(Vector3 position)
	{
		return TileToPosition(PositionToTile(position));
	}

	public static GameObject[] GetObjectsOnTile(Tile tile)
	{
		return GetObjectsOnTile(tile.coordinates);
	}

	public static GameObject[] GetObjectsOnTile(Vector2Int coordinates)
	{
		Vector3 tilePos = TileToPosition(coordinates);

		RaycastHit2D[] hits = Physics2D.RaycastAll(tilePos, Vector3.forward, 10);
		GameObject[] result = new GameObject[hits.Length];

		for (int i = 0; i < hits.Length; i++)
			result[i] = hits[i].collider.gameObject;

		return result;
	}

	public static int GetTileDistance(Vector2Int t1, Vector2Int t2)
	{
		return Mathf.Abs(t1.x - t2.x) + Mathf.Abs(t1.y - t2.y);
	}



	public static Vector2Int[] GetPointArea(Vector2Int point, int radius)
	{
		throw new System.NotImplementedException();
	}

	public static Vector2Int[,] GetSquareArea(Vector2Int point, int radius)
	{
		int edgeLength = radius * 2 + 1;
		Vector2Int[,] result = new Vector2Int[edgeLength, edgeLength];

		for (int y = -radius; y <= radius; y++)
			for (int x = -radius; x <= radius; x++)
				result[x, y] = new Vector2Int(point.x + x, point.y + y);

		return result;
	}

	public static Vector2Int[] GetTileLine(Vector2Int start, Vector2Int finish)
	{
		throw new System.NotImplementedException();
	}
}
