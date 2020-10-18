using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
	public EnemyManager enemyManager;

	private bool playerTurn = true;

	public void Btn_EndTurn()
	{
		EndPlayerTurn();
	}

	private void EndPlayerTurn()
	{
		// enemy takes a turn
		enemyManager.TakeTurn(this);

		playerTurn = false;

	}

	public void EndAITurn()
	{
		// reset player units
		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject pu in playerUnits)
			if (pu.GetComponent<PlayerUnit>() != null)
				pu.GetComponent<PlayerUnit>().ResetTurn();

		playerTurn = true;
	}

	private void Update()
	{
		Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// DEBUG
		Debug.DrawLine(mousePosWorld - new Vector3(0.2f, 0.2f), mousePosWorld + new Vector3(0.2f, 0.2f), Color.red);
		Debug.DrawLine(mousePosWorld - new Vector3(-0.2f, 0.2f), mousePosWorld + new Vector3(-0.2f, 0.2f), Color.red);
	}
}
