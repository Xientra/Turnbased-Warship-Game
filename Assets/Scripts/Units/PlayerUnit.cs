using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
	public Ghost ghostPrefab;
	private Ghost ghost;

	public LineAttack attackPrefab;

	private Camera cam;


	private bool moving = false;
	private bool usingAbility = false;
	public bool PerformingAction { get => moving || usingAbility; }


	void Start()
	{
		cam = Camera.main;

		ResetTurn(); // TODO should later happen in the Turn Manager
	}

	void Update()
	{
		Vector3 mousePosWorld = cam.ScreenToWorldPoint(Input.mousePosition);

		if (moving == true)
		{
			ghost.transform.position = new Vector3(Mathf.Round(mousePosWorld.x), Mathf.Round(mousePosWorld.y), 0);

			ghost.lineRenderer.SetPosition(0, ghost.transform.position);
			ghost.lineRenderer.SetPosition(1, transform.position);
			ghost.lineRenderer.enabled = true;


			int tilesMoved = Mathf.Abs((int)ghost.transform.position.x - (int)transform.position.x) + Mathf.Abs((int)ghost.transform.position.y - (int)transform.position.y);

			if (tilesMoved > movementRemaining)
				ghost.lineRenderer.enabled = false;
		}
	}

	public void Select()
	{
		Debug.LogWarning("Select in PlayerUnit is not implemented");
	}

	public void Deselect()
	{
		Debug.LogWarning("Deselect in PlayerUnit is not implemented");
	}

	public void StartMoving()
	{
		ghost = Instantiate(ghostPrefab.gameObject, transform.position, transform.rotation).GetComponent<Ghost>();
		moving = true;
	}

	public void MoveTo(Vector3 point)
	{
		if (moving)
		{
			if (ghost != null)
			{
				int tilesMoved = Mathf.Abs((int)ghost.transform.position.x - (int)transform.position.x) + Mathf.Abs((int)ghost.transform.position.y - (int)transform.position.y);

				if (movementRemaining - tilesMoved >= 0)
				{
					movementRemaining -= tilesMoved;

					transform.position = ghost.transform.position;
					moving = false;
					Destroy(ghost.gameObject);
				}
			}
		}
	}

	public void CancelMoving()
	{
		if (moving)
		{
			moving = false;
			Destroy(ghost.gameObject);
		}
	}
}
