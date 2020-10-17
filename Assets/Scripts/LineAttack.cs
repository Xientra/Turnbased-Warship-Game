using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttack : MonoBehaviour
{
	public GameObject visual;

	public void Activate(Vector3Int startPos, Vector3 direction, int length)
	{
		transform.position = startPos;

		direction.Normalize();

		for (int i = 0; i < length; i++)
			Instantiate(visual, GridUtility.RoundVector3(startPos + direction * i) + new Vector3(0.5f, 0.5f, 0), visual.transform.rotation, transform);

		Destroy(this.gameObject, 1.2f);
	}
}
