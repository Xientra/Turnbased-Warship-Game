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
		EndTurn(true);
	}

	private void EndTurn(bool playerTurn)
	{
		// enemy takes a turn
		enemyManager.TakeTurn();


		// reset player units
		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject pu in playerUnits)
			if (pu.GetComponent<PlayerUnit>() != null)
				pu.GetComponent<PlayerUnit>().ResetTurn();
	}

	private void Update()
	{
		Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// DEBUG
		Debug.DrawLine(mousePosWorld - new Vector3(0.2f, 0.2f), mousePosWorld + new Vector3(0.2f, 0.2f), Color.red);
		Debug.DrawLine(mousePosWorld - new Vector3(-0.2f, 0.2f), mousePosWorld + new Vector3(-0.2f, 0.2f), Color.red);
	}
}
