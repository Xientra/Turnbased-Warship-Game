﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAbility : Ability
{
	public override bool Activate(Unit origin)
	{
		// calculate direction
		Vector3 direction = Vector3.up;
		Vector3 mouseTransDiff = targetPosition - origin.transform.position;

		if (mouseTransDiff.x > Mathf.Abs(mouseTransDiff.y))
			direction = Vector3.right;
		else if (-mouseTransDiff.x > Mathf.Abs(mouseTransDiff.y))
			direction = Vector3.left;
		if (mouseTransDiff.y > Mathf.Abs(mouseTransDiff.x))
			direction = Vector3.up;
		else if (-mouseTransDiff.y > Mathf.Abs(mouseTransDiff.x))
			direction = Vector3.down;


		// starts at 1 so that the line begins one tile off the origin tile (where the firing unit is)
		float effectiveRange = range == -1 ? 200 : range;
		for (int i = 1; i <= effectiveRange; i++)
		{
			Tile t = new Tile(origin.transform.position + direction * i);

			Instantiate(hitVisual, t.Position, hitVisual.transform.rotation, transform);

			Unit unitOnTile = GridUtility.GetUnitOnTile(t);
			if (unitOnTile != null)
				if (unitOnTile.faction != origin.faction || friendlyFire == true)
					unitOnTile.ChangeHealth(damageAndHeal);
		}

		Destroy(this.gameObject, 1.2f);
		return true;
	}
}
