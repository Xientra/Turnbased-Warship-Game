using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAbility : Ability
{
	public override bool Activating(Unit origin, Vector3 mouseWorldPos)
	{
		if (range == -1 || Tile.Distance(Tile.PositionToCoordinates(origin.transform.position), Tile.PositionToCoordinates(mouseWorldPos)) <= range)
		{
			GridVisual.singelton.DrawLine(origin.transform.position, mouseWorldPos);
			GridVisual.singelton.MarkLine(GridUtility.GetTileLine(new Tile(origin.transform.position), new Tile(mouseWorldPos)));
		}
		else
		{
			Debug.Log("Out of range");
			return false;
		}

		return true;
	}

	public override bool Activate(Unit origin, Vector3 mouseWorldPos)
	{
		if (range == -1 || Tile.Distance(Tile.PositionToCoordinates(origin.transform.position), Tile.PositionToCoordinates(mouseWorldPos)) <= range)
		{
			Tile[] line = GridUtility.GetTileLine(new Tile(origin.transform.position), new Tile(mouseWorldPos));

			// starts at 1 so that the line begins one tile off the origin tile (where the firing unit is)
			for (int i = 1; i < line.Length; i++)
			{
				Instantiate(hitVisual, line[i].Position, hitVisual.transform.rotation, transform);

				Unit unitOnTile = GridUtility.GetUnitOnTile(line[i]);
				if (unitOnTile != null)
					if (unitOnTile.faction != origin.faction || friendlyFire == true)
						unitOnTile.ChangeHealth(damageAndHeal);
			}
		}
		else
			return false;

		Destroy(this.gameObject, 1.2f);
		return true;
	}
}
