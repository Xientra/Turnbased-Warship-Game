using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
	public Camera cam;
	public LineRenderer lineRenderer;
	public GameObject targetTileMarker;

	private enum AbilityVisuals { none, Point, CrossDirection, CrossPoint, MultiDirection, FreeDirection }
	private AbilityVisuals activeAbilityVisual = AbilityVisuals.none;

	public bool IsActivatingAbility { get => (activeAbilityVisual != AbilityVisuals.none); }

	private Vector3 targetPosition;
	private Vector3 direction;

	Ability abilityPrefab;
	Unit origin;

	public void UseAbility(Unit origin, Ability ability)
	{
		this.abilityPrefab = ability;
		this.origin = origin;

		if (ability is LineAbility)
			activeAbilityVisual = AbilityVisuals.CrossDirection;
		else if (ability is PointAbility)
			activeAbilityVisual = AbilityVisuals.Point;
		else if (ability is CrossPointAbility)
			activeAbilityVisual = AbilityVisuals.CrossPoint;
	}

	#region Activating Ability

	void Update()
	{
		switch (activeAbilityVisual)
		{
			case AbilityVisuals.Point:
				PointVisual();
				break;
			case AbilityVisuals.CrossDirection:
				CrossDirectionVisual();
				break;
			case AbilityVisuals.CrossPoint:
				CrossPointVisual();
				break;
			case AbilityVisuals.MultiDirection:
				break;
			case AbilityVisuals.FreeDirection:
				break;


			default:

				break;
		}

		if (IsActivatingAbility)
		{
			if (Input.GetMouseButtonDown(0))
			{
				ActivateAbility();
			}
		}
	}

	/// <summary> Highlights a specific tile </summary>
	private void PointVisual()
	{
		Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

		targetTileMarker.transform.position = Tile.SnapToGrid(mouseWorldPos);

		if (abilityPrefab.range != -1 && Tile.Distance(Tile.PositionToCoordinates(origin.transform.position), Tile.PositionToCoordinates(mouseWorldPos)) > abilityPrefab.range)
		{
			Debug.Log("Out of range");
		}

		targetTileMarker.SetActive(true);
	}

	/// <summary> Highlights only one of the four (x, -x, y, -y) directions </summary>
	private void CrossDirectionVisual()
	{
		Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

		Vector3 mouseTransDiff = mouseWorldPos - origin.transform.position;

		if (mouseTransDiff.x > Mathf.Abs(mouseTransDiff.y))
			direction = new Vector3(1, 0, 0);
		else if (-mouseTransDiff.x > Mathf.Abs(mouseTransDiff.y))
			direction = new Vector3(-1, 0, 0);
		if (mouseTransDiff.y > Mathf.Abs(mouseTransDiff.x))
			direction = new Vector3(0, 1, 0);
		else if (-mouseTransDiff.y > Mathf.Abs(mouseTransDiff.x))
			direction = new Vector3(0, -1, 0);

		float visualRange = abilityPrefab.range == -1 ? 200 : (abilityPrefab.range);

		lineRenderer.SetPosition(0, Tile.SnapToGrid(origin.transform.position));
		lineRenderer.SetPosition(1, Tile.SnapToGrid(origin.transform.position) + direction.normalized * visualRange);
		lineRenderer.enabled = true;
	}

	/// <summary> Highlights a point, that also lies on one of the four (x, -x, y, -y) directions </summary>
	private void CrossPointVisual()
	{
		PointVisual();
		CrossDirectionVisual();

		Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

		Vector2Int targetCoords = Tile.PositionToCoordinates(mouseWorldPos);
		Vector2Int originCoords = Tile.PositionToCoordinates(origin.transform.position);


		if (targetCoords.x == originCoords.x || targetCoords.y == originCoords.y)
		{
			// in coss
		}
		else
		{
			Debug.Log("Not in cross");
		}
	}

	#endregion

	private void ActivateAbility()
	{
		if (origin.actionPointsRemaining - abilityPrefab.actionPointCost < 0)
		{
			Debug.Log("not enougth action points");
			return;
		}

		Ability ability = Instantiate(abilityPrefab.gameObject, origin.transform.position, abilityPrefab.gameObject.transform.rotation).GetComponent<Ability>();

		ability.targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		
		bool succsess = ability.Activate(origin);
		if (succsess)
			origin.actionPointsRemaining -= ability.actionPointCost;

		activeAbilityVisual = AbilityVisuals.none;
		HideAllVisuals();
	}

	public void CancelActivation()
	{
		activeAbilityVisual = AbilityVisuals.none;
		HideAllVisuals();
	}

	private void HideAllVisuals()
	{
		lineRenderer.enabled = false;
		targetTileMarker.SetActive(false);
	}
}
