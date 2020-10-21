using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
	public Camera cam;
	public LineRenderer lineRenderer;

	private bool activating = false;

	private Vector3 targetPosition;
	private Vector3 direction;
	private int length;

	Ability abilityPrefab;
	Unit origin;

	public void UseAbility(Unit origin, Ability ability)
	{
		this.abilityPrefab = ability;
		this.origin = origin;

		activating = true;
	}

	void Update()
	{
		if (activating)
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

	
			if (Input.GetMouseButtonDown(0))
			{
				UseLineAbility();
			}
		}
	}

	private void UseLineAbility()
	{
		LineAttack la = (LineAttack)abilityPrefab;
		LineAttack actualAbility = Instantiate(la.gameObject, origin.transform.position, la.gameObject.transform.rotation).GetComponent<LineAttack>();

		actualAbility.Activate(origin, direction);

		activating = false;
		lineRenderer.enabled = false;
	}
}
