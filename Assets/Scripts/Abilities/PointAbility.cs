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


	public override void Activate(Unit origin)
	{
		Vector2Int targetTile = GridUtility.PositionToTile(targetPosition);

		int activationDistance = GridUtility.GetTileDistance(GridUtility.PositionToTile(origin.transform.position), targetTile);

		if (activationDistance <= range || range == 0)
		{
			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(targetTile);

			Instantiate(visual, GridUtility.TileToPosition(targetTile), visual.transform.rotation, transform);

			for (int j = 0; j < objectsOnTile.Length; j++)
			{
				Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
				if (unitOnTile != null)
					unitOnTile.TakeDamage(damage);
			}
		}

		Destroy(this.gameObject, 1.2f);
	}
}
