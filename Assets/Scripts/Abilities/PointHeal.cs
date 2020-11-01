using UnityEngine;

public class PointHeal : Ability
{
	[Header("Point Heal")]

	public int heal = 1;

	public override void Activate(Unit origin)
	{
		Vector2Int targetTile = GridUtility.PositionToTile(targetPosition);

		int activationDistance = GridUtility.GetTileDistance(GridUtility.PositionToTile(origin.transform.position), targetTile);

		if (activationDistance <= range || range == -1)
		{
			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(targetTile);

			Instantiate(visual, GridUtility.TileToPosition(targetTile), visual.transform.rotation, transform);

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
