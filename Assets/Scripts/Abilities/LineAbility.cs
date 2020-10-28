using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAbility : Ability
{
	[Header("Properties")]

	public int length;
	public int damage;

	[Space(5)]

	public Vector3 direction;

	public override void Activate(Unit origin)
	{
		transform.position = GridUtility.SnapToGrid(transform.position + direction); // + direction, so that it starts one tile away from the origin

		for (int i = 0; i < length; i++)
		{
			Vector2Int tilePos = GridUtility.PositionToTile(origin.transform.position + direction * i);

			Instantiate(visual, GridUtility.TileToPosition(tilePos), visual.transform.rotation, transform);

			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(tilePos);
			for (int j = 0; j < objectsOnTile.Length; j++)
			{
				Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
				if (unitOnTile != null && unitOnTile != origin)
					unitOnTile.TakeDamage(damage);
			}
		}


		Destroy(this.gameObject, 1.2f);
	}
}
