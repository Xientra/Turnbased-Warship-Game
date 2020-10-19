using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[Header("Resources:")]

	[Tooltip("How far the unit can be moved in tiles.")]
	public int movementPerRound = 4;
	public int movementRemaining = 0;

	public int maxHealth = 4;
	public int health;


	public int actionPointsPerRound = 1;
	public int actionPointsRemaining = 0;

	[Header("Abilities:")]

	public Ability[] abilities;


	private void Awake()
	{
		health = maxHealth;
	}

	public void ResetTurn()
	{
		movementRemaining = movementPerRound;
		actionPointsRemaining = actionPointsPerRound;
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		if (health <= 0)
		{
			health = 0;
			Die();
		}
	}

	public void Die()
	{
		Destroy(this.gameObject);
	}
}
