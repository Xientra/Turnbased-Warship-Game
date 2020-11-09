using UnityEngine;

public class PlayerUnit : Unit
{

	[Header("Player Unit:")]
	public Ghost ghost;

	private Camera cam;


	private bool moving = false;
	private bool usingAbility = false;
	public bool PerformingAction { get => moving || usingAbility; }


	void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		if (moving == true)
		{
			Vector3 mousePosWorld = cam.ScreenToWorldPoint(Input.mousePosition);

			ghost.transform.position = GridUtility.SnapToGrid(mousePosWorld);

			ghost.lineRenderer.SetPosition(0, ghost.transform.position);
			ghost.lineRenderer.SetPosition(1, transform.position);
			ghost.lineRenderer.enabled = true;


			int tilesMoved = (int)GridUtility.GetTileDistance(GridUtility.PositionToTile(ghost.transform.position), GridUtility.PositionToTile(transform.position));

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
		ghost.gameObject.SetActive(true);
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
					ghost.gameObject.SetActive(false);
				}
			}
		}
	}

	public void CancelMoving()
	{
		if (moving)
		{
			moving = false;
			ghost.gameObject.SetActive(false);
		}
	}
}
