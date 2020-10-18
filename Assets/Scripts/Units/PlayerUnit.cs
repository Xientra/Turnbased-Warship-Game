using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
	public Ghost ghostPrefab;
	private Ghost ghost;

	public LineAttack attackPrefab;

	private Camera cam;
	private bool dragging = false;


	void Start()
	{
		cam = Camera.main;

		ResetTurn(); // TODO should later happen in the Turn Manager
	}

	void Update()
	{
		Vector3 mousePosWorld = cam.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
			if (dragging == false)
			{
				RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosWorld, Vector3.forward, 1);
				foreach (RaycastHit2D hit in hits)
					if (hit.collider.gameObject == this.gameObject)
					{
						ghost = Instantiate(ghostPrefab.gameObject, transform.position, transform.rotation).GetComponent<Ghost>();
						dragging = true;
					}
			}
			else
			{
				int tilesMoved = Mathf.Abs((int)ghost.transform.position.x - (int)transform.position.x) + Mathf.Abs((int)ghost.transform.position.y - (int)transform.position.y);

				if (remainingMovement - tilesMoved >= 0)
				{
					remainingMovement -= tilesMoved;

					transform.position = ghost.transform.position;
					dragging = false;
					Destroy(ghost.gameObject);
				}
			}
		}

		if (dragging == true)
		{
			ghost.transform.position = new Vector3(Mathf.Round(mousePosWorld.x), Mathf.Round(mousePosWorld.y), 0);

			ghost.lineRenderer.SetPosition(0, ghost.transform.position);
			ghost.lineRenderer.SetPosition(1, transform.position);
			ghost.lineRenderer.enabled = true;


			int tilesMoved = Mathf.Abs((int)ghost.transform.position.x - (int)transform.position.x) + Mathf.Abs((int)ghost.transform.position.y - (int)transform.position.y);

			if (tilesMoved > remainingMovement)
				ghost.lineRenderer.enabled = false;
		}


		// cancel drag
		if (Input.GetMouseButtonDown(1))
		{
			if (dragging)
			{
				dragging = false;
				Destroy(ghost.gameObject);
			}
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			LineAttack attack = Instantiate(attackPrefab);
			attack.Activate(this, GridUtility.RoundVector3(transform.position + Vector3.right), Vector3.right);
		}
	}

	private void OnMouseDown()
	{
		//Debug.Log("Mouse Down");
	}

	private void OnMouseUp()
	{
		//Debug.Log("Mouse Up");
	}
}
