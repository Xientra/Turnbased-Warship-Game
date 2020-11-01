using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

	[Header("Visual")]
	public GameObject visual;

	[Header("Ability Base:")]

	public bool isInstant = false;

	public int actionPointCost = 1;

	[Tooltip("How far away the target ability can be activated. -1 is infinite.")]
	public int range = -1;

	

	[HideInInspector]
	public Unit targetUnit;
	[HideInInspector]
	public Vector3 targetPosition;

	public abstract void Activate(Unit origin);
}
