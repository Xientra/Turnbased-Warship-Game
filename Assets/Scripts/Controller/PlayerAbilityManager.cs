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

	private Ability abilityPrefab;
	private Ability ability;
	private Unit origin;

	public void UseAbility(Unit origin, Ability ability)
	{
		// create and save everything needed for the activation.
		this.abilityPrefab = ability;
		this.ability = Instantiate(abilityPrefab, origin.transform.position, Quaternion.identity);
		this.origin = origin;

		// is now activating
		activeAbilityVisual = AbilityVisuals.Point;

		/*
		if (ability is LineAbility)
			activeAbilityVisual = AbilityVisuals.CrossDirection;
		else if (ability is PointAbility)
			activeAbilityVisual = AbilityVisuals.Point;
		else if (ability is CrossPointAbility)
			activeAbilityVisual = AbilityVisuals.CrossPoint;
			*/
	}

	#region Activating Ability

	void Update()
	{
		/*
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
		*/


		if (IsActivatingAbility)
		{
			Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
			mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

			bool canBeActivated = ability.Activating(origin, mouseWorldPos);

			if (canBeActivated && Input.GetMouseButtonDown(0))
			{
				ActivateAbility();

				// stop activating
			}
		}
	}


	/// <summary> Highlights a point, that also lies on one of the four (x, -x, y, -y) directions </summary>
	private void CrossPointVisual()
	{
		//PointVisual();
		//CrossDirectionVisual();

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

		Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

		Ability ability = Instantiate(abilityPrefab.gameObject, origin.transform.position, abilityPrefab.gameObject.transform.rotation).GetComponent<Ability>();

		ability.targetPosition = mouseWorldPos;
		
		bool succsess = ability.Activate(origin, mouseWorldPos);
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
