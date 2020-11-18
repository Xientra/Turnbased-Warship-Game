using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PointAbility : Ability
{

	[Header("Point Ability")]

	public int inaccuracy = 0;
	//[Tooltip("The radius of the area around the target tile, that is affected")]
	//public int radius = 1;


	public override bool Activate(Unit origin)
	{
		Tile targetTile = new Tile(targetPosition);

		int activationDistance = targetTile.Distance(Tile.PositionToCoordinates(origin.transform.position));

		if (activationDistance <= range || range == -1)
		{
			targetTile = ApplyInaccuracy();

			Instantiate(hitVisual, targetTile.Position, hitVisual.transform.rotation, transform);

			Unit unitOnTile = GridUtility.GetUnitOnTile(targetTile);
			if (unitOnTile != null)
				if (unitOnTile.faction != origin.faction || friendlyFire == true)
					unitOnTile.ChangeHealth(damageAndHeal);
		}
		else
			return false;

		Destroy(this.gameObject, 1.2f);
		return true;
	}

	private Tile ApplyInaccuracy()
	{
		targetPosition += new Vector3(Random.Range(-inaccuracy, inaccuracy + 1), Random.Range(-inaccuracy, inaccuracy + 1), 0);
		return new Tile(targetPosition);
	}
}
