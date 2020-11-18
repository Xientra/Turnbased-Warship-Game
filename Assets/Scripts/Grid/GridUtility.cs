using System;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtility
{
	public static Unit GetUnitOnTile(Tile tile)
	{
		GameObject[] objectsOnTile = GetObjectsOnTile(tile);

		for (int j = 0; j < objectsOnTile.Length; j++)
		{
			Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
			if (unitOnTile != null)
				return unitOnTile;
		}

		return null;
	}

	public static GameObject[] GetObjectsOnTile(Tile tile)
	{
		return GetObjectsOnTile(tile.coordinates);
	}

	private static GameObject[] GetObjectsOnTile(Vector2Int coordinates)
	{
		Vector3 tilePos = Tile.CoordinatesToPosition(coordinates);

		RaycastHit2D[] hits = Physics2D.RaycastAll(tilePos, Vector3.forward, 10);
		GameObject[] result = new GameObject[hits.Length];

		for (int i = 0; i < hits.Length; i++)
			result[i] = hits[i].collider.gameObject;

		return result;
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

	public static Tile[] GetTileLine(Tile start, Tile end)
	{
		List<Tile> result = new List<Tile>();

		Vector2Int absDif = new Vector2Int(Mathf.Abs(start.coordinates.x - end.coordinates.x), Mathf.Abs(start.coordinates.y - end.coordinates.y));
		float steps;

		if (absDif.x > absDif.y)
			steps = absDif.x;
		else
			steps = absDif.y;


		for (int i = 0; i <= steps; i++)
		{
			Vector3 pos = Vector3.Lerp(start.Position, end.Position, i / steps);
			result.Add(new Tile(pos));
			//Debug.DrawLine(start.Position, pos);
		}

		return result.ToArray();
	}
}
