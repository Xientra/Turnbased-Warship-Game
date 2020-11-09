using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPointAbility : Ability
{
	public override bool Activate(Unit origin)
	{
		Tile targetTile = new Tile(targetPosition);
		Vector2Int originCoords = Tile.PositionToCoordinates(origin.transform.position);

		if (targetTile.coordinates.x == originCoords.x || targetTile.coordinates.y == originCoords.y)
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
			else
				return false;
		}
		else
			return false;


		Destroy(this.gameObject, 1.2f);
		return true;
	}
}
