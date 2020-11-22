using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

	[Header("Visual")]
	public GameObject hitVisual;

	[Header("Ability Base:")]

	public bool friendlyFire = true;

	public int actionPointCost = 1;

	[Tooltip("How far away the target ability can be activated. -1 is infinite.")]
	public float range = -1;

	[Tooltip("Negative is Damage and positive is Heal.")]
	public int damageAndHeal = -2;

	// these will be set in script

	[HideInInspector]
	public Unit targetUnit;
	[HideInInspector]
	public Vector3 targetPosition;


	/// <summary> Is displaying the visual for the ability. </summary>
	/// <param name="origin"> The Unity that activated this ability. </param>
	/// <param name="mouseWorldPos"> The current position of the mouse in world space coordinates. </param>
	/// <returns> Wheather or not the ability can currently be used. </returns>
	public abstract bool Activating(Unit origin, Vector3 mouseWorldPos);

	/// <summary> Activates the Ability. </summary>
	/// <param name="origin"> The Unity that activated this ability. </param>
	/// <param name="mouseWorldPos"> The current position of the mouse in world space coordinates. </param>
	/// <returns> Wheather or not the activation was successfull. </returns>
	public abstract bool Activate(Unit origin, Vector3 mouseWorldPos);
}
