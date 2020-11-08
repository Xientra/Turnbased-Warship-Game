﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PointAttack : PointAbility
{

	[Header("Point Attack")]

	public int damage = 2;
	public int inaccuracy = 2;
	//[Tooltip("The radius of the area around the target tile, that is affected")]
	//public int radius = 1;


	public override void Activate(Unit origin)
	{
		Tile targetTile = new Tile(targetPosition);

		int activationDistance = targetTile.Distance(Tile.PositionToCoordinates(origin.transform.position));

		if (activationDistance <= range || range == -1)
		{
			targetTile = ApplyInaccuracy();

			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(targetTile);

			Instantiate(visual, targetTile.Position, visual.transform.rotation, transform);

			for (int j = 0; j < objectsOnTile.Length; j++)
			{
				Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
				if (unitOnTile != null)
					unitOnTile.TakeDamage(damage);
			}
		}

		Destroy(this.gameObject, 1.2f);
	}

	private Tile ApplyInaccuracy()
	{
		targetPosition += new Vector3(Random.Range(-inaccuracy, inaccuracy + 1), Random.Range(-inaccuracy, inaccuracy + 1), 0);
		return new Tile(targetPosition);
	}
}