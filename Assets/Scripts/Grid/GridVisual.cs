using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class GridVisual : MonoBehaviour
{
	public static GridVisual singelton;

	public LineRenderer lineRenderer;

	public Vector3 vs = new Vector3(0, 0, 0);
	public Vector3 vt = new Vector3(6, 6, 0);

	private void Awake()
	{
		singelton = this;
	}

	void Start()
	{

	}

	void Update()
	{
		/*
		Tile s = new Tile(vs);
		Tile t = new Tile(vt);

		DebugDrawX(s.Position, 0.3f, Color.yellow);
		DebugDrawX(t.Position, 0.3f, Color.yellow);

		Tile[] tiles = GridUtility.GetTileLine(s, t);
		for (int i = 0; i < tiles.Length; i++)
		{
			DebugDrawX(tiles[i].Position, 0.5f, Color.red);
		}
		*/
	}

	private void LateUpdate()
	{

	}

	public void MarkTile(Tile tile)
	{
		DebugDrawX(tile.Position, 0.5f, Color.red);
	}

	public void MarkLine(Tile[] tiles)
	{
		for (int i = 0; i < tiles.Length; i++)
			DebugDrawX(tiles[i].Position, 0.5f, Color.red);
	}

	public void DrawLine(Tile from, Tile to)
	{
		Debug.DrawLine(from.Position, to.Position, Color.red);
		//lineRenderer.enabled = true;
		//lineRenderer.positionCount = 2;
		//lineRenderer.SetPosition(0, from.Position);
		//lineRenderer.SetPosition(1, to.Position);
	}
	public void DrawLine(Vector3 from, Vector3 to)
	{
		Debug.DrawLine(from, to, Color.red);
	}

	private void OnDrawGizmos()
	{
		/*
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
		*/
	}

	private void DebugDrawX(Vector3 pos, float size, Color color)
	{
		Debug.DrawLine(pos - new Vector3(size, size), pos + new Vector3(size, size), color);
		Debug.DrawLine(pos - new Vector3(-size, size), pos + new Vector3(-size, size), color);
	}
}
