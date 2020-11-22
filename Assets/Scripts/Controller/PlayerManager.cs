using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerAbilityManager))]
public class PlayerManager : MonoBehaviour
{
	[SerializeField]
	private GameObject selectionMarker;

	public PlayerUnit selectedUnit;

	private List<PlayerUnit> units;

	private PlayerAbilityManager abilityManager;
	private Camera cam;

	[Header("UI:")]

	public TMP_Text abilitiesLabel;
	public TMP_Text selectionInfoLabel;
	public TMP_Text hoverInfoLabel;

	private void Start()
	{
		cam = Camera.main;
		abilityManager = GetComponent<PlayerAbilityManager>();
		ClearAbilitiesLabel();
		ClearLabel(selectionInfoLabel);
		ClearLabel(hoverInfoLabel);
	}

	public void TakeTurn()
	{
		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject pu in playerUnits)
			if (pu.GetComponent<PlayerUnit>() != null)
				pu.GetComponent<PlayerUnit>().ResetTurn();
	}

	private void Update()
	{
		Vector3 mousePosWorld = cam.ScreenToWorldPoint(Input.mousePosition);

		RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosWorld, Vector3.forward, 1);
		Unit unitHoveringOver = null;
		foreach (RaycastHit2D hit in hits)
		{
			Unit u = hit.collider.GetComponent<Unit>();
			if (u != null)
				unitHoveringOver = u;
		}


		if (Input.GetMouseButtonDown(0))
		{
			if (unitHoveringOver != null && unitHoveringOver is PlayerUnit)
			{
				if (selectedUnit == null && unitHoveringOver != selectedUnit)
				{
					SelectUnit((PlayerUnit)unitHoveringOver);
				}
				else if (selectedUnit.PerformingAction == false && unitHoveringOver == selectedUnit)
				{
					selectedUnit.StartMoving();
				}
			}

			if (unitHoveringOver == null && selectedUnit != null)
			{
				// move unit
				selectedUnit.MoveTo(mousePosWorld);
				selectionMarker.transform.position = selectedUnit.transform.position;

				// make unit abilities usable in UI
			}
		}

		if (selectedUnit != null)
		{
			if (Input.GetMouseButtonDown(1))
			{
				if (selectedUnit.PerformingAction == true)
				{
					selectedUnit.CancelMoving();
				}
				else if (abilityManager.IsActivatingAbility == true)
				{
					abilityManager.CancelActivation();
				}
				else
				{
					DeselectUnit();
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha1))
				abilityManager.UseAbility(selectedUnit, selectedUnit.abilities[0]);
			if (Input.GetKeyDown(KeyCode.Alpha2))
				abilityManager.UseAbility(selectedUnit, selectedUnit.abilities[1]);
			if (Input.GetKeyDown(KeyCode.Alpha3))
				abilityManager.UseAbility(selectedUnit, selectedUnit.abilities[2]);
			if (Input.GetKeyDown(KeyCode.Alpha4))
				abilityManager.UseAbility(selectedUnit, selectedUnit.abilities[3]);
		}


		// hovering effect
		if (unitHoveringOver != null)
		{
			SetInfoLabel(unitHoveringOver, hoverInfoLabel);
		}
		else
			ClearLabel(hoverInfoLabel);



		// DEBUG
		Debug.DrawLine(mousePosWorld - new Vector3(0.2f, 0.2f), mousePosWorld + new Vector3(0.2f, 0.2f), Color.red);
		Debug.DrawLine(mousePosWorld - new Vector3(-0.2f, 0.2f), mousePosWorld + new Vector3(-0.2f, 0.2f), Color.red);
	}

	public void SelectUnit(PlayerUnit unit)
	{
		selectedUnit = unit;
		SetAbilitiesLabel();
		SetInfoLabel(unit, selectionInfoLabel);
		selectionMarker.transform.position = selectedUnit.transform.position;
		selectionMarker.SetActive(true);
	}

	public void DeselectUnit()
	{
		ClearAbilitiesLabel();
		ClearLabel(selectionInfoLabel);
		selectedUnit = null;
		selectionMarker.SetActive(false);
	}



	// ============================== UI ============================== //

	public void SetAbilitiesLabel()
	{
		abilitiesLabel.text = "";

		for (int i = 0; i < selectedUnit.abilities.Length; i++)
			abilitiesLabel.text += (i + 1) + ": " + (selectedUnit.abilities[i] != null ? selectedUnit.abilities[i].name : "[NO DATA]") + "\n";
	}

	public void ClearAbilitiesLabel()
	{
		abilitiesLabel.text = "[NO DATA]";
	}

	public void SetInfoLabel(Unit unit, TMP_Text label)
	{
		label.text =
		"Name: " + unit.name + "\n" +
		"Health: " + unit.health + " / " + unit.maxHealth + "\n" +
		"Action Points: " + unit.actionPointsRemaining + " / " + unit.actionPointsPerRound + "\n" +
		"Movement: " + unit.movementRemaining + " / " + unit.movementPerRound;
	}

	public void ClearLabel(TMP_Text label)
	{
		label.text = "";
	}

	public void Btn_EndTurn()
	{
		TurnManager.singelton.EndPlayerTurn();
	}
}
