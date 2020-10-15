using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
	public Vector2 halfsize = new Vector2(10, 5);
	public Color color;

	void Start()
	{

	}

	void Update()
	{

	}

	private void OnDrawGizmos_NOT()
	{
		for (int y = (int)-halfsize.y; y <= halfsize.y; y++)
		{
			for (int x = (int)-halfsize.x; x <= halfsize.x; x++)
			{
				Gizmos.color = color;
				Gizmos.DrawLine(transform.position + new Vector3(x, halfsize.y, 0), transform.position + new Vector3(x, -halfsize.y, 0));
			}
			Gizmos.color = color;
			Gizmos.DrawLine(transform.position + new Vector3(-halfsize.x, y, 0), transform.position + new Vector3(halfsize.x, y, 0));
		}
	}
}
