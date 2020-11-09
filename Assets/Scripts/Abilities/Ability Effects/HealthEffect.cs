using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Effect", menuName = "Ability Effects/Health Effect")]
public class HealthEffect : AbilityEffect
{
	[Tooltip("Amount to change health by. Negative is damage positive is healing.")]
	public int amount = -5;

	public override void AppyEffect(Unit target)
	{
		target.ChangeHealth(amount);
	}
}
