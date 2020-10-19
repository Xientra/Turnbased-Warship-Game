using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtility
{
	public static Vector3Int RoundVector3(Vector3 pos)
	{
		return new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
	}

	public static GameObject[] GetObjectsOnTile(Vector3Int tile)
	{
		Vector3 pos = tile + new Vector3(0.5f, 0.5f, 0);

		RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector3.forward, 10);
		GameObject[] result = new GameObject[hits.Length];

		for (int i = 0; i < hits.Length; i++)
			result[i] = hits[i].collider.gameObject;

		return result;
	}
}
