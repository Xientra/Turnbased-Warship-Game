using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPointHeal : PointAbility
{
	public override void Activate(Unit origin)
	{
		Tile targetTile = new Tile(targetPosition);

		if (targetTile.Position.x == origin.transform.position.x || targetTile.Position.y == origin.transform.position.y)
		{
			int activationDistance = targetTile.Distance(Tile.PositionToCoordinates(origin.transform.position));

			if (activationDistance <= range || range == -1)
			{
				GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(targetTile);

				Instantiate(hitVisual, targetTile.Position, hitVisual.transform.rotation, transform);

				for (int j = 0; j < objectsOnTile.Length; j++)
				{
					Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
					if (unitOnTile != null)
						effect.AppyEffect(unitOnTile);
				}
			}
		}
		else
			Debug.Log("No in cross");

		Destroy(this.gameObject, 1.2f);
	}
}
