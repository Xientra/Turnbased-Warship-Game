using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class EnemyUnit : Unit
{

	public void TakeTurn()
	{
		PlayerUnit target = FindPlayerUnit();

		Vector2Int tilesDistance = new Vector2Int((int)target.transform.position.x - (int)transform.position.x, (int)target.transform.position.y - (int)transform.position.y);

		if (tilesDistance.y > 0)
		{
			MoveY(tilesDistance.y);
			//Move(new Vector2Int(-tilesDistance.x, 0));
		}
		else if(tilesDistance.y < 0)
		{
			MoveY(tilesDistance.y);
			//Move(new Vector2Int(tilesDistance.x, 0));
		}

	}

	public void MoveY(int distance)
	{
		if (distance > movementRemaining)
			distance = movementRemaining;
		else if (Mathf.Abs(distance) > movementRemaining)
			distance = -movementRemaining;

		transform.position += new Vector3(0, distance, 0);

		movementRemaining -= distance;
	}


	public PlayerUnit FindNearestPlayerUnit()
	{
		GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < playerObjs.Length; i++)
		{
			if (playerObjs[i].GetComponent<PlayerUnit>() != null)
			{
				
			}
		}
		return null;
	}

	public PlayerUnit FindPlayerUnit()
	{
		GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < playerObjs.Length; i++)
			if (playerObjs[i].GetComponent<PlayerUnit>() != null)
				return playerObjs[i].GetComponent<PlayerUnit>();
		return null;
	}
}
