using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtility
{
	public static Vector3Int RoundVector3(Vector3 pos)
	{
		return new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
	}
}
