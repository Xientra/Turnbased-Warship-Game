using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[Tooltip("How far the unit can be moved in tiles.")]
	public int movementPerRound = 4;
	protected int remainingMovement = 0;

	public int maxHealth = 4;
	protected int health;

	private void Awake()
	{
		health = maxHealth;
	}

	public void ResetTurn()
	{
		remainingMovement = movementPerRound;
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
