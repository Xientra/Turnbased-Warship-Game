using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
	public static TurnManager singelton;

	public EnemyManager enemyManager;
	public PlayerManager playerManager;

	private bool playerTurn = true;

	private void Awake()
	{
		singelton = this;
	}

	private void Start()
	{
		EndAITurn();
	}

	public void EndPlayerTurn()
	{
		// enemy takes a turn
		enemyManager.TakeTurn();

		playerTurn = false;
	}

	public void EndAITurn()
	{
		// reset player units
		playerManager.TakeTurn();

		playerTurn = true;
	}
}
