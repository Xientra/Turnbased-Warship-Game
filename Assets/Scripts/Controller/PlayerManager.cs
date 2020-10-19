using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

	public PlayerUnit selectedUnit;

	private List<PlayerUnit> units;

	private Camera cam;

	[Header("UI:")]

	public TMP_Text abilitiesLabel;
	public TMP_Text resourcesLabel;

	private void Start()
	{
		cam = Camera.main;
		ClearAbilitiesLabel();
		ClearStatsLabel();
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
			bool selectedNewUnit = false;

			if (unitHoveringOver != null && unitHoveringOver is PlayerUnit && unitHoveringOver != selectedUnit)
			{
				SelectUnit((PlayerUnit)unitHoveringOver);
				selectedNewUnit = true;
			}

			if (selectedUnit != null && selectedNewUnit == false)
			{
				// move unit
				selectedUnit.Move(mousePosWorld);

				// make unit abilities usable in UI
			}
		}

		if (selectedUnit != null)
		{
			if (Input.GetMouseButtonDown(1))
			{
				// unselect unit
				DeselectUnit();
				// cancel possible movement
			}

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				selectedUnit.UseAbility(1);
			}
		}


		// hovering effect
		if (unitHoveringOver != null)
		{
			SetStatsLabel(unitHoveringOver);
		}
		else ClearStatsLabel();



		// DEBUG
		Debug.DrawLine(mousePosWorld - new Vector3(0.2f, 0.2f), mousePosWorld + new Vector3(0.2f, 0.2f), Color.red);
		Debug.DrawLine(mousePosWorld - new Vector3(-0.2f, 0.2f), mousePosWorld + new Vector3(-0.2f, 0.2f), Color.red);
	}

	public void SelectUnit(PlayerUnit unit)
	{
		selectedUnit = unit;
		selectedUnit.Select();
		SetAbilitiesLabel();
	}

	public void DeselectUnit()
	{
		selectedUnit.Deselect();
		ClearAbilitiesLabel();
		selectedUnit = null;
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

	public void SetStatsLabel(Unit unit)
	{
		resourcesLabel.text =
		"Name: " + unit.name + "\n" +
		"Health: " + unit.health + " / " + unit.maxHealth + "\n" +
		"Action Points: " + unit.actionPointsRemaining + " / " + unit.actionPointsPerRound + "\n" +
		"Movement: " + unit.movementRemaining + " / " + unit.movementPerRound;
	}

	public void ClearStatsLabel()
	{
		resourcesLabel.text = "";
	}

	public void Btn_EndTurn()
	{
		TurnManager.singelton.EndPlayerTurn();
	}
}
