using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

	[Header("Visual")]
	public GameObject visual;

	[Header("Ability Base:")]

	public int actionPointCost = 1;

	public bool isInstant = false;


	public abstract void Activate(Unit origin);
}
