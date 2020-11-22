using UnityEngine;

public class PointAbility : Ability
{

	[Header("Point Ability")]

	public int inaccuracy = 0;
	//[Tooltip("The radius of the area around the target tile, that is affected")]
	//public int radius = 1;

	/// <summary> Will be called in PlayerAbilityManager repeatedly unitl the ability is used </summary>
	public override bool Activating(Unit origin, Vector3 mouseWorldPos)
	{
		//mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

		GridVisual.singelton.MarkTile(new Tile(mouseWorldPos));

		if (range != -1 && Tile.Distance(Tile.PositionToCoordinates(origin.transform.position), Tile.PositionToCoordinates(mouseWorldPos)) > range)
		{
			Debug.Log("Out of range");
			return false;
		}
		else
		{
			GridVisual.singelton.DrawLine(origin.transform.position, mouseWorldPos);
		}

		return true;
	}

	public override bool Activate(Unit origin, Vector3 mouseWorldPos)
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
