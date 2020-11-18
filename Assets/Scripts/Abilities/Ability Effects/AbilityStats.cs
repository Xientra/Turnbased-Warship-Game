using UnityEngine;

[System.Serializable]
public class AbilityStats
{
	public int damage = 0;
	public int heal = 0;

	public void AppyEffect(Unit target)
	{
		target.ChangeHealth(heal - damage);
	}
}
