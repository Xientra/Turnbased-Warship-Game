using UnityEngine;

public class PointHeal : PointAbility
{
	[Header("Point Heal")]

	public int heal = 1;

	public override void Activate(Unit origin)
	{
		Tile targetTile = new Tile(targetPosition);

		int activationDistance = targetTile.Distance(Tile.PositionToCoordinates(origin.transform.position));

		if (activationDistance <= range || range == -1)
		{
			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(targetTile);

			Instantiate(visual, targetTile.Position, visual.transform.rotation, transform);

			for (int j = 0; j < objectsOnTile.Length; j++)
			{
				Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
				if (unitOnTile != null)
					unitOnTile.TakeDamage(-heal);
			}
		}

		Destroy(this.gameObject, 1.2f);
	}
}
