using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAbility : Ability
{

	[Header("Point Attack")]

	public int damage = 2;

	[Tooltip("How far away the target Tile can be. 0 is infinite")]
	public int range = 10;

	//[Tooltip("The radius of the area around the target tile, that is affected")]
	//public int radius = 1;

	[Space(5)]

	public Vector3Int targetTile;



	public override void Activate(Unit origin)
	{
		int activationDistance = (int)GridUtility.GetTileDistance(origin.transform.position, targetTile);

		if (activationDistance <= range)
		{
			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(targetTile);

			for (int j = 0; j < objectsOnTile.Length; j++)
			{
				Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
				if (unitOnTile != null)
					unitOnTile.TakeDamage(damage);
			}
		}
	}
}
