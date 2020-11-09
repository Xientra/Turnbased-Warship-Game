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

		if (ability is LineAttack)
			activeAbilityVisual = AbilityVisuals.CrossDirection;
		else if (ability is PointAbility)
		{
			activeAbilityVisual = AbilityVisuals.Point;
			if (ability is CrossPointHeal)
				activeAbilityVisual = AbilityVisuals.CrossPoint;
		}
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

		targetTileMarker.transform.position = GridUtility.SnapToGrid(mouseWorldPos);

		if (abilityPrefab.range != -1 && GridUtility.GetTileDistance(GridUtility.PositionToTile(origin.transform.position), GridUtility.PositionToTile(mouseWorldPos)) > abilityPrefab.range)
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

		int visualRange = abilityPrefab.range == -1 ? 200 : (abilityPrefab.range);

		lineRenderer.SetPosition(0, GridUtility.SnapToGrid(origin.transform.position));
		lineRenderer.SetPosition(1, GridUtility.SnapToGrid(origin.transform.position) + direction.normalized * visualRange);
		lineRenderer.enabled = true;
	}

	/// <summary> Highlights a point, that also lies on one of the four (x, -x, y, -y) directions </summary>
	private void CrossPointVisual()
	{
		PointVisual();
		CrossDirectionVisual();
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
		//ability.targetUnit = ;
		ability.Activate(origin);

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
