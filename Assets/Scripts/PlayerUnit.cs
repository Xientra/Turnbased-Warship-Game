using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
	public Ghost ghostPrefab;
	private Ghost ghost;

	private Camera cam;
	private bool dragging = false;


	[Tooltip("How far the ship can be moved in tiles.")]
	public int movementPerRound = 4;
	private int remainingMovement = 0;


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
				RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosWorld, mousePosWorld + new Vector3(0, 0, 1), 200);
				foreach (RaycastHit2D hit in hits)
					if (hit.collider.CompareTag("Player"))
					{
						ghost = Instantiate(ghostPrefab.gameObject, transform.position, transform.rotation).GetComponent<Ghost>();
						dragging = true;
					}
			}
			else
			{
				int tilesMoved = Mathf.Abs((int)ghost.transform.position.x - (int)transform.position.x) + Mathf.Abs((int)ghost.transform.position.y - (int)transform.position.y);
				remainingMovement -= tilesMoved;

				if (remainingMovement >= 0)
				{
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

		// DEBUG
		Debug.DrawLine(mousePosWorld - new Vector3(0.2f, 0.2f), mousePosWorld + new Vector3(0.2f, 0.2f), Color.red);
		Debug.DrawLine(mousePosWorld - new Vector3(-0.2f, 0.2f), mousePosWorld + new Vector3(-0.2f, 0.2f), Color.red);
	}

	private void OnMouseDown()
	{
		//Debug.Log("Mouse Down");
	}

	private void OnMouseUp()
	{
		//Debug.Log("Mouse Up");
	}


	public void ResetTurn()
	{
		remainingMovement = movementPerRound;
	}
}
