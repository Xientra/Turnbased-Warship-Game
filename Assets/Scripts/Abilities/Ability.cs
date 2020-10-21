using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
	public int actionPointCost = 1;

	public bool isInstant = false;

	public void Activate(Unit origin) { }
}
