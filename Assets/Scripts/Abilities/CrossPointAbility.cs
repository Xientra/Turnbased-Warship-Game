using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPointAbility : Ability
{
	public override bool Activating(Unit origin, Vector3 mouseWorldPos)
	{
		throw new System.NotImplementedException();
	}

	public override bool Activate(Unit origin, Vector3 mouseWorldPos)
	{
		Tile targetTile = new Tile(targetPosition);
		Vector2Int originCoords = Tile.PositionToCoordinates(origin.transform.position);

		if (targetTile.coordinates.x == originCoords.x || targetTile.coordinates.y == originCoords.y)
		{
			int activationDistance = targetTile.Distance(Tile.PositionToCoordinates(origin.transform.position));

			if (activationDistance <= range || range == -1)
			{
				Instantiate(hitVisual, targetTile.Position, hitVisual.transform.rotation, transform);

				Unit unitOnTile = GridUtility.GetUnitOnTile(targetTile);
				if (unitOnTile != null)
					if (unitOnTile.faction != origin.faction || friendlyFire == true)
						unitOnTile.ChangeHealth(damageAndHeal);
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
