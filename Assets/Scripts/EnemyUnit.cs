using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Video;

public class EnemyUnit : MonoBehaviour
{
	[Tooltip("How far the unit can be moved in tiles.")]
	public int movementPerRound = 4;
	private int remainingMovement = 0;

	public void ResetTurn()
	{
		remainingMovement = movementPerRound;
	}

	public void TakeTurn()
	{
		PlayerUnit target = FindPlayerUnit();

		Vector2Int tilesDistance = new Vector2Int((int)target.transform.position.x - (int)transform.position.x, (int)target.transform.position.y - (int)transform.position.y);

		if (tilesDistance.x > 0)
		{
			MoveX(tilesDistance.x);
			//Move(new Vector2Int(-tilesDistance.x, 0));
		}
		else if(tilesDistance.x < 0)
		{
			MoveX(tilesDistance.x);
			//Move(new Vector2Int(tilesDistance.x, 0));
		}

	}

	public void MoveX(int distance)
	{
		if (distance > remainingMovement)
			distance = remainingMovement;
		else if (Mathf.Abs(distance) > remainingMovement)
			distance = -remainingMovement;

		transform.position += new Vector3(distance, 0, 0);

		remainingMovement -= distance;
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
