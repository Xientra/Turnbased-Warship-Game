﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttack : Ability
{
	public LineRenderer lineRenderer;

	public GameObject visual;
	public int length;
	public int damage;

	public void Activate(Unit origin, Vector3 direction)
	{
		transform.position = GridUtility.RoundVector3(transform.position + direction); // + direction, so that it starts one tile away from the origin

		for (int i = 0; i < length; i++)
		{
			Vector3Int tilePos = GridUtility.RoundVector3(origin.transform.position + direction * i);

			Instantiate(visual, tilePos + new Vector3(0.5f, 0.5f, 0), visual.transform.rotation, transform);

			GameObject[] objectsOnTile = GridUtility.GetObjectsOnTile(tilePos);
			for (int j = 0; j < objectsOnTile.Length; j++)
			{
				Unit unitOnTile = objectsOnTile[j].GetComponent<Unit>();
				if (unitOnTile != null && unitOnTile != origin)
					unitOnTile.TakeDamage(damage);
			}
		}


		Destroy(this.gameObject, 1.2f);
	}
}