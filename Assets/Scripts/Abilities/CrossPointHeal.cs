using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPointHeal : PointAbility
{
	[Header("Cross Point Heal:")]

	public int heal = 2;


	public override void Activate(Unit origin)
	{
		Tile targetTile = new Tile(targetPosition);

		if (targetTile.Position.x == origin.transform.position.x || targetTile.Position.y == origin.transform.position.y)
		{
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
			else
				Debug.Log("To far away");
		}
		else
			Debug.Log("No in cross");

		Destroy(this.gameObject, 1.2f);
	}
}
