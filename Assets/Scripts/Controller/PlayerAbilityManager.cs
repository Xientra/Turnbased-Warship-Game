using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
	public Camera cam;
	public LineRenderer lineRenderer;
	public GameObject targetTileMarker;

	private enum ActivatingAbility { none, Point, CrossDirection, CrossPoint, MultiDirection, FreeDirection }
	private ActivatingAbility activatingAbility = ActivatingAbility.none;

	public bool IsActivatingAbility { get => (activatingAbility == ActivatingAbility.none); }

	private Vector3 targetPosition;
	private Vector3 direction;
	private int length;

	Ability abilityPrefab;
	Unit origin;

	public void UseAbility(Unit origin, Ability ability)
	{
		this.abilityPrefab = ability;
		this.origin = origin;

		//activatingAbility = true; // okay, but how is the Ability Manager supposed to know what what ability is... isn't it better to do it in the ability after all?
	}

	#region Activating Ability

	void Update()
	{
		switch (activatingAbility)
		{
			case ActivatingAbility.Point:
				UpdatePointAbility();
				break;
			case ActivatingAbility.CrossDirection:
				UpdateCrossDirectionAbility();
				break;
			case ActivatingAbility.CrossPoint:
				UpdateCrossPointAbility();
				break;
			case ActivatingAbility.MultiDirection:
				break;
			case ActivatingAbility.FreeDirection:
				break;


			default:

				break;
		}

		if (IsActivatingAbility)
		{
			if (Input.GetMouseButtonDown(0))
			{
				UseLineAbility();
			}
		}
	}

	/// <summary> A Ability, that targets a specific tile on the grid </summary>
	private void UpdatePointAbility()
	{

	}

	/// <summary> A Ability, that only wants one of the four (x, -x, y, -y) directions </summary>
	private void UpdateCrossDirectionAbility()
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

		lineRenderer.SetPosition(0, origin.transform.position + new Vector3(0.5f, 0.5f, 0));
		lineRenderer.SetPosition(1, origin.transform.position + new Vector3(0.5f, 0.5f, 0) + direction.normalized * (((LineAttack)abilityPrefab).length - 1));
		lineRenderer.enabled = true;
	}

	/// <summary> A Ability, that wants a point, that also lies on one of the four (x, -x, y, -y) directions </summary>
	private void UpdateCrossPointAbility()
	{

	}

	#endregion

	private void UseLineAbility()
	{
		LineAttack la = (LineAttack)abilityPrefab;
		LineAttack actualAbility = Instantiate(la.gameObject, origin.transform.position, la.gameObject.transform.rotation).GetComponent<LineAttack>();

		actualAbility.direction = direction;
		actualAbility.Activate(origin);

		activatingAbility = ActivatingAbility.none;
		lineRenderer.enabled = false;
	}

	public void CancelActivation()
	{
		activatingAbility = ActivatingAbility.none;
		lineRenderer.enabled = false;
	}
}
